using Autodesk.AutoCAD.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AcApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace Batch_Scripter
{
    public partial class MainForm : Form
    {
        private readonly List<string> drawingFiles = new List<string>();
        private readonly ToolTip toolTip = new ToolTip();

        public MainForm()
        {
            InitializeComponent();
            BatchScripterTheme.Apply(
                this,
                btnRunScript,
                footerPanel,
                Logo,
                GitHub,
                LinkedIn);
            ConfigureFooterLinks();
            TryApplyWindowIcon();
            BatchScriptBuilder.DeleteStaleScripts(TimeSpan.FromDays(7));
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            try
            {
                Document activeDocument = AcApp.DocumentManager.MdiActiveDocument;
                if (activeDocument != null)
                    activeDocument.Window.Focus();
                else
                    AcApp.MainWindow.Focus();
            }
            catch
            {
                // Focus recovery must never prevent the form from closing.
            }
        }

        private void btnBrowseDrawings_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.AutoUpgradeEnabled = true;
                    dialog.CheckFileExists = true;
                    dialog.Filter = "AutoCAD drawings and templates (*.dwg;*.dwt)|*.dwg;*.dwt|All files (*.*)|*.*";
                    dialog.Multiselect = true;
                    dialog.Title = "Add Drawings";

                    if (dialog.ShowDialog(this) != DialogResult.OK)
                        return;

                    foreach (string file in dialog.FileNames)
                    {
                        string fullPath = Path.GetFullPath(file);
                        if (drawingFiles.Any(existing =>
                            string.Equals(existing, fullPath, StringComparison.OrdinalIgnoreCase)))
                        {
                            continue;
                        }

                        drawingFiles.Add(fullPath);
                        listBoxDrawings.Items.Add(fullPath);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Unable to add drawings", ex);
            }
        }

        private void btnRemoveDrawings_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (object item in listBoxDrawings.SelectedItems.Cast<object>().ToList())
                {
                    string file = item.ToString();
                    drawingFiles.RemoveAll(existing =>
                        string.Equals(existing, file, StringComparison.OrdinalIgnoreCase));
                    listBoxDrawings.Items.Remove(item);
                }
            }
            catch (Exception ex)
            {
                ShowError("Unable to remove drawings", ex);
            }
        }

        private void btnBrowseScript_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog dialog = new OpenFileDialog())
                {
                    dialog.AutoUpgradeEnabled = true;
                    dialog.CheckFileExists = true;
                    dialog.Filter = "AutoCAD scripts (*.scr)|*.scr|Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    dialog.Multiselect = false;
                    dialog.Title = "Add Script";

                    if (dialog.ShowDialog(this) != DialogResult.OK)
                        return;

                    string content = File.ReadAllText(dialog.FileName);
                    if (textBoxContents.TextLength > 0 &&
                        !textBoxContents.Text.EndsWith(Environment.NewLine, StringComparison.Ordinal))
                    {
                        textBoxContents.AppendText(Environment.NewLine);
                    }

                    textBoxContents.AppendText(content);
                }
            }
            catch (Exception ex)
            {
                ShowError("Unable to add the script", ex);
            }
        }

        private void btnRunScript_Click(object sender, EventArgs e)
        {
            if (drawingFiles.Count == 0)
            {
                ShowMessage("No Drawings Selected",
                    "Add one or more drawing files before running the script.",
                    MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrWhiteSpace(textBoxContents.Text))
            {
                ShowMessage("No Script Entered",
                    "Add or enter a script before running the batch.",
                    MessageBoxIcon.Information);
                return;
            }

            List<string> missingFiles = drawingFiles.Where(file => !File.Exists(file)).ToList();
            if (missingFiles.Count > 0)
            {
                ShowMessage("Drawing Not Found",
                    "The following drawing files could not be found:" + Environment.NewLine +
                    Environment.NewLine + string.Join(Environment.NewLine, missingFiles),
                    MessageBoxIcon.Warning);
                return;
            }

            List<string> openDrawings = GetOpenBatchDrawings();
            if (openDrawings.Count > 0)
            {
                ShowMessage("Drawings Already Open",
                    "Close these drawings before starting the batch:" + Environment.NewLine +
                    Environment.NewLine + string.Join(Environment.NewLine, openDrawings),
                    MessageBoxIcon.Warning);
                return;
            }

            string saveMessage = chkSaveDrawings.Checked
                ? "Changes will be saved."
                : "Changes will be discarded when each drawing closes.";

            DialogResult result = ThemedMessageBox.Show(
                this,
                "Run the script on " + drawingFiles.Count + " drawing(s)?" +
                Environment.NewLine + Environment.NewLine + saveMessage,
                "Run Batch Script",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (result != DialogResult.Yes)
                return;

            try
            {
                // Build the complete script synchronously on AutoCAD's UI thread. The
                // previous Task.Run implementation read WinForms controls and invoked
                // AutoCAD APIs from a worker thread, which is not supported.
                string scriptFile = BatchScriptBuilder.CreateTemporaryScript(
                    drawingFiles,
                    textBoxContents.Text,
                    chkSaveDrawings.Checked);

                Document activeDocument = AcApp.DocumentManager.MdiActiveDocument;
                if (activeDocument == null)
                    throw new InvalidOperationException("No active AutoCAD drawing is available.");

                // SCRIPT normally opens a file picker while FILEDIA is enabled. The
                // proven Batch Tool launcher supplies the path through AutoLISP so
                // the user's FILEDIA setting does not need to be changed. Do not add
                // trailing whitespace: it becomes another Enter after SCRIPT starts.
                string command = BuildScriptLauncher(scriptFile);

                Hide();
                activeDocument.Window.Focus();
                activeDocument.SendStringToExecute(command, true, false, false);
                Close();
            }
            catch (Exception ex)
            {
                Show();
                ShowError("Unable to start the batch script", ex);
            }
        }

        private static string BuildScriptLauncher(string scriptFile)
        {
            string autoLispPath = scriptFile
                .Replace("\\", "\\\\")
                .Replace("\"", "\\\"");
            return "(command \"_.SCRIPT\" \"" + autoLispPath + "\")";
        }

        private List<string> GetOpenBatchDrawings()
        {
            HashSet<string> openFiles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (Document document in AcApp.DocumentManager.Cast<Document>())
            {
                try
                {
                    openFiles.Add(Path.GetFullPath(document.Name));
                }
                catch
                {
                    openFiles.Add(document.Name);
                }
            }

            return drawingFiles.Where(file => openFiles.Contains(Path.GetFullPath(file))).ToList();
        }

        private void ConfigureFooterLinks()
        {
            toolTip.SetToolTip(GitHub, "Oliver's GitHub");
            toolTip.SetToolTip(LinkedIn, "Oliver Wackenreuther on LinkedIn");
            toolTip.SetToolTip(Logo, "Batch Scripter");
            GitHub.Cursor = Cursors.Hand;
            LinkedIn.Cursor = Cursors.Hand;
        }

        private void TryApplyWindowIcon()
        {
            try
            {
                using (Stream iconStream = typeof(MainForm).Assembly
                    .GetManifestResourceStream("Batch_Scripter.Logo_BW_Small.ico"))
                {
                    if (iconStream != null)
                    {
                        using (System.Drawing.Icon sourceIcon = new System.Drawing.Icon(iconStream))
                            Icon = (System.Drawing.Icon)sourceIcon.Clone();
                    }
                }
            }
            catch
            {
                // The form remains usable if the optional icon cannot be loaded.
            }
        }

        private void linkLblFootnote_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl("https://ca.linkedin.com/in/oliverwackenreuther");
        }

        private void linkLblLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl("https://github.com/OliversDev/Batch-Scripter-App-for-AutoCAD/blob/master/LICENSE.txt");
        }

        private void linkLblHelp_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenUrl("https://github.com/OliversDev/Batch-Scripter-App-for-AutoCAD/blob/master/README.md");
        }

        private void GitHub_Click(object sender, EventArgs e)
        {
            OpenUrl("https://github.com/OliversDev");
        }

        private void LinkedIn_Click(object sender, EventArgs e)
        {
            OpenUrl("https://ca.linkedin.com/in/oliverwackenreuther");
        }

        private void OpenUrl(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                ThemedMessageBox.Show(
                    this,
                    "Unable to open the link: " + ex.Message,
                    "Batch Scripter - Link Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ShowMessage(string title, string message, MessageBoxIcon icon)
        {
            ThemedMessageBox.Show(this, message, "Batch Scripter - " + title,
                MessageBoxButtons.OK, icon);
        }

        private void ShowError(string message, Exception ex)
        {
            string details = message + ": " + ex.Message;
            LogError(details);
            ShowMessage("Error", details, MessageBoxIcon.Error);
        }

        private static void LogError(string message)
        {
            try
            {
                string logFolder = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "Batch Scripter",
                    "Logs");
                Directory.CreateDirectory(logFolder);
                string logPath = Path.Combine(logFolder,
                    "BatchScripter-" + DateTime.Now.ToString("yyyyMMdd") + ".log");
                File.AppendAllText(logPath,
                    DateTime.Now.ToString("O") + " " + message + Environment.NewLine);
            }
            catch
            {
                // Logging must not mask the original failure.
            }
        }
    }
}
