using AutoCADApplication = Autodesk.AutoCAD.ApplicationServices.Application;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Batch_Scripter
{
    internal static class BatchScripterTheme
    {
        private static readonly Color Accent = Color.FromArgb(0, 120, 212);
        private static readonly Color Footer = Color.FromArgb(37, 37, 38);

        public static void Apply(Form form, Button primaryButton, Panel footerPanel)
        {
            bool isDark = IsDarkMode();
            Color background = isDark ? Color.FromArgb(30, 30, 30) : Color.FromArgb(245, 245, 245);
            Color surface = isDark ? Color.FromArgb(45, 45, 48) : Color.White;
            Color input = isDark ? Color.FromArgb(37, 37, 38) : Color.White;
            Color text = isDark ? Color.FromArgb(245, 245, 245) : Color.FromArgb(32, 32, 32);
            Color border = isDark ? Color.FromArgb(80, 80, 84) : Color.FromArgb(190, 190, 190);

            form.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point);
            form.BackColor = background;
            form.ForeColor = text;

            ApplyToChildren(form, background, surface, input, text, border);

            primaryButton.BackColor = Accent;
            primaryButton.ForeColor = Color.White;
            primaryButton.FlatAppearance.BorderColor = Accent;
            primaryButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(16, 110, 190);
            primaryButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(0, 90, 158);

            // The permanent dark footer preserves the original white logo and
            // GitHub icon in both AutoCAD light and dark themes.
            footerPanel.BackColor = Footer;
            footerPanel.ForeColor = Color.White;
            foreach (Control child in footerPanel.Controls)
            {
                if (!(child is Button))
                {
                    child.BackColor = Footer;
                    child.ForeColor = Color.White;
                }

                LinkLabel link = child as LinkLabel;
                if (link != null)
                {
                    link.LinkColor = Color.FromArgb(86, 156, 214);
                    link.ActiveLinkColor = Color.FromArgb(230, 174, 16);
                    link.VisitedLinkColor = link.LinkColor;
                }
            }
        }

        private static void ApplyToChildren(
            Control parent,
            Color background,
            Color surface,
            Color input,
            Color text,
            Color border)
        {
            foreach (Control control in parent.Controls)
            {
                control.ForeColor = text;

                if (control is GroupBox)
                {
                    control.BackColor = background;
                }
                else if (control is TextBox || control is ListBox)
                {
                    control.BackColor = input;
                }
                else if (control is Button)
                {
                    Button button = (Button)control;
                    button.UseVisualStyleBackColor = false;
                    button.FlatStyle = FlatStyle.Flat;
                    button.FlatAppearance.BorderColor = border;
                    button.FlatAppearance.BorderSize = 1;
                    button.BackColor = surface;
                    button.ForeColor = text;
                }
                else if (!(control is PictureBox) && !(control is LinkLabel))
                {
                    control.BackColor = background;
                }

                if (control.HasChildren)
                    ApplyToChildren(control, background, surface, input, text, border);
            }
        }

        private static bool IsDarkMode()
        {
            try
            {
                object value = AutoCADApplication.GetSystemVariable("COLORTHEME");
                return Convert.ToInt32(value) == 0;
            }
            catch
            {
                return true;
            }
        }
    }
}
