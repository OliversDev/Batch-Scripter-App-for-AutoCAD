namespace Batch_Scripter
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.GroupBox grpDrawings;
        private System.Windows.Forms.ListBox listBoxDrawings;
        private System.Windows.Forms.Button btnBrowseDrawings;
        private System.Windows.Forms.Button btnRemoveDrawings;
        private System.Windows.Forms.GroupBox grpScript;
        private System.Windows.Forms.TextBox textBoxContents;
        private System.Windows.Forms.Button btnBrowseScript;
        private System.Windows.Forms.Panel footerPanel;
        private System.Windows.Forms.CheckBox chkSaveDrawings;
        private System.Windows.Forms.Button btnRunScript;
        private System.Windows.Forms.LinkLabel linkLblFootnote;
        private System.Windows.Forms.PictureBox Logo;
        private System.Windows.Forms.PictureBox GitHub;
        private System.Windows.Forms.PictureBox LinkedIn;
        private System.Windows.Forms.LinkLabel linkLblLicense;
        private System.Windows.Forms.LinkLabel linkLblPrivacy;
        private System.Windows.Forms.LinkLabel linkLblHelp;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
                toolTip.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.grpDrawings = new System.Windows.Forms.GroupBox();
            this.listBoxDrawings = new System.Windows.Forms.ListBox();
            this.btnBrowseDrawings = new System.Windows.Forms.Button();
            this.btnRemoveDrawings = new System.Windows.Forms.Button();
            this.grpScript = new System.Windows.Forms.GroupBox();
            this.textBoxContents = new System.Windows.Forms.TextBox();
            this.btnBrowseScript = new System.Windows.Forms.Button();
            this.footerPanel = new System.Windows.Forms.Panel();
            this.chkSaveDrawings = new System.Windows.Forms.CheckBox();
            this.btnRunScript = new System.Windows.Forms.Button();
            this.linkLblFootnote = new System.Windows.Forms.LinkLabel();
            this.Logo = new System.Windows.Forms.PictureBox();
            this.GitHub = new System.Windows.Forms.PictureBox();
            this.LinkedIn = new System.Windows.Forms.PictureBox();
            this.linkLblLicense = new System.Windows.Forms.LinkLabel();
            this.linkLblPrivacy = new System.Windows.Forms.LinkLabel();
            this.linkLblHelp = new System.Windows.Forms.LinkLabel();
            this.grpDrawings.SuspendLayout();
            this.grpScript.SuspendLayout();
            this.footerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GitHub)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LinkedIn)).BeginInit();
            this.SuspendLayout();
            //
            // grpDrawings
            //
            this.grpDrawings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDrawings.Controls.Add(this.listBoxDrawings);
            this.grpDrawings.Controls.Add(this.btnBrowseDrawings);
            this.grpDrawings.Controls.Add(this.btnRemoveDrawings);
            this.grpDrawings.Location = new System.Drawing.Point(10, 10);
            this.grpDrawings.Name = "grpDrawings";
            this.grpDrawings.Size = new System.Drawing.Size(940, 267);
            this.grpDrawings.TabIndex = 0;
            this.grpDrawings.TabStop = false;
            this.grpDrawings.Text = "Drawing Files";
            //
            // listBoxDrawings
            //
            this.listBoxDrawings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxDrawings.FormattingEnabled = true;
            this.listBoxDrawings.HorizontalScrollbar = true;
            this.listBoxDrawings.IntegralHeight = false;
            this.listBoxDrawings.Location = new System.Drawing.Point(10, 24);
            this.listBoxDrawings.Name = "listBoxDrawings";
            this.listBoxDrawings.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBoxDrawings.Size = new System.Drawing.Size(920, 193);
            this.listBoxDrawings.TabIndex = 0;
            //
            // btnBrowseDrawings
            //
            this.btnBrowseDrawings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseDrawings.Location = new System.Drawing.Point(670, 227);
            this.btnBrowseDrawings.Name = "btnBrowseDrawings";
            this.btnBrowseDrawings.Size = new System.Drawing.Size(125, 30);
            this.btnBrowseDrawings.TabIndex = 1;
            this.btnBrowseDrawings.Text = "Add Drawings";
            this.btnBrowseDrawings.Click += new System.EventHandler(this.btnBrowseDrawings_Click);
            //
            // btnRemoveDrawings
            //
            this.btnRemoveDrawings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveDrawings.Location = new System.Drawing.Point(805, 227);
            this.btnRemoveDrawings.Name = "btnRemoveDrawings";
            this.btnRemoveDrawings.Size = new System.Drawing.Size(125, 30);
            this.btnRemoveDrawings.TabIndex = 2;
            this.btnRemoveDrawings.Text = "Remove Drawings";
            this.btnRemoveDrawings.Click += new System.EventHandler(this.btnRemoveDrawings_Click);
            //
            // grpScript
            //
            this.grpScript.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpScript.Controls.Add(this.textBoxContents);
            this.grpScript.Controls.Add(this.btnBrowseScript);
            this.grpScript.Location = new System.Drawing.Point(10, 287);
            this.grpScript.Name = "grpScript";
            this.grpScript.Size = new System.Drawing.Size(940, 293);
            this.grpScript.TabIndex = 1;
            this.grpScript.TabStop = false;
            this.grpScript.Text = "Script";
            //
            // textBoxContents
            //
            this.textBoxContents.AcceptsReturn = true;
            this.textBoxContents.AcceptsTab = true;
            this.textBoxContents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxContents.Font = new System.Drawing.Font("Consolas", 9F);
            this.textBoxContents.Location = new System.Drawing.Point(10, 24);
            this.textBoxContents.Multiline = true;
            this.textBoxContents.Name = "textBoxContents";
            this.textBoxContents.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxContents.Size = new System.Drawing.Size(920, 219);
            this.textBoxContents.TabIndex = 0;
            this.textBoxContents.WordWrap = false;
            //
            // btnBrowseScript
            //
            this.btnBrowseScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBrowseScript.Location = new System.Drawing.Point(805, 253);
            this.btnBrowseScript.Name = "btnBrowseScript";
            this.btnBrowseScript.Size = new System.Drawing.Size(125, 30);
            this.btnBrowseScript.TabIndex = 1;
            this.btnBrowseScript.Text = "Add Script";
            this.btnBrowseScript.Click += new System.EventHandler(this.btnBrowseScript_Click);
            //
            // footerPanel
            //
            this.footerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.footerPanel.Controls.Add(this.chkSaveDrawings);
            this.footerPanel.Controls.Add(this.btnRunScript);
            this.footerPanel.Controls.Add(this.linkLblFootnote);
            this.footerPanel.Controls.Add(this.Logo);
            this.footerPanel.Controls.Add(this.GitHub);
            this.footerPanel.Controls.Add(this.LinkedIn);
            this.footerPanel.Controls.Add(this.linkLblLicense);
            this.footerPanel.Controls.Add(this.linkLblPrivacy);
            this.footerPanel.Controls.Add(this.linkLblHelp);
            this.footerPanel.Location = new System.Drawing.Point(0, 590);
            this.footerPanel.Name = "footerPanel";
            this.footerPanel.Size = new System.Drawing.Size(960, 60);
            this.footerPanel.TabIndex = 2;
            //
            // chkSaveDrawings
            //
            this.chkSaveDrawings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSaveDrawings.AutoSize = true;
            this.chkSaveDrawings.Checked = true;
            this.chkSaveDrawings.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSaveDrawings.Location = new System.Drawing.Point(652, 21);
            this.chkSaveDrawings.Name = "chkSaveDrawings";
            this.chkSaveDrawings.Size = new System.Drawing.Size(106, 19);
            this.chkSaveDrawings.TabIndex = 6;
            this.chkSaveDrawings.Text = "Save drawings";
            this.chkSaveDrawings.UseVisualStyleBackColor = false;
            //
            // btnRunScript
            //
            this.btnRunScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRunScript.Location = new System.Drawing.Point(815, 15);
            this.btnRunScript.Name = "btnRunScript";
            this.btnRunScript.Size = new System.Drawing.Size(125, 30);
            this.btnRunScript.TabIndex = 7;
            this.btnRunScript.Text = "Run Script";
            this.btnRunScript.Click += new System.EventHandler(this.btnRunScript_Click);
            //
            // Logo
            //
            this.Logo.Image = global::Batch_Scripter.Properties.Resources.Logo_BW_NOBG;
            this.Logo.Location = new System.Drawing.Point(10, 15);
            this.Logo.Name = "Logo";
            this.Logo.Size = new System.Drawing.Size(53, 30);
            this.Logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Logo.TabStop = false;
            //
            // GitHub
            //
            this.GitHub.Location = new System.Drawing.Point(71, 15);
            this.GitHub.Name = "GitHub";
            this.GitHub.Size = new System.Drawing.Size(30, 30);
            this.GitHub.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.GitHub.TabStop = false;
            this.GitHub.Click += new System.EventHandler(this.GitHub_Click);
            //
            // LinkedIn
            //
            this.LinkedIn.Location = new System.Drawing.Point(109, 15);
            this.LinkedIn.Name = "LinkedIn";
            this.LinkedIn.Size = new System.Drawing.Size(30, 30);
            this.LinkedIn.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.LinkedIn.TabStop = false;
            this.LinkedIn.Click += new System.EventHandler(this.LinkedIn_Click);
            //
            // linkLblFootnote
            //
            this.linkLblFootnote.AutoSize = true;
            this.linkLblFootnote.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.linkLblFootnote.Location = new System.Drawing.Point(151, 23);
            this.linkLblFootnote.Name = "linkLblFootnote";
            this.linkLblFootnote.Size = new System.Drawing.Size(224, 15);
            this.linkLblFootnote.TabIndex = 2;
            this.linkLblFootnote.TabStop = true;
            this.linkLblFootnote.Text = "Created by Oliver Wackenreuther, v2.0";
            this.linkLblFootnote.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLblFootnote_LinkClicked);
            //
            // linkLblLicense
            //
            this.linkLblLicense.AutoSize = true;
            this.linkLblLicense.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.linkLblLicense.Location = new System.Drawing.Point(390, 23);
            this.linkLblLicense.Name = "linkLblLicense";
            this.linkLblLicense.Size = new System.Drawing.Size(44, 15);
            this.linkLblLicense.TabIndex = 3;
            this.linkLblLicense.TabStop = true;
            this.linkLblLicense.Text = "License";
            this.linkLblLicense.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLblLicense_LinkClicked);
            //
            // linkLblPrivacy
            //
            this.linkLblPrivacy.AutoSize = true;
            this.linkLblPrivacy.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.linkLblPrivacy.Location = new System.Drawing.Point(449, 23);
            this.linkLblPrivacy.Name = "linkLblPrivacy";
            this.linkLblPrivacy.Size = new System.Drawing.Size(44, 15);
            this.linkLblPrivacy.TabIndex = 4;
            this.linkLblPrivacy.TabStop = true;
            this.linkLblPrivacy.Text = "Privacy";
            this.linkLblPrivacy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLblPrivacy_LinkClicked);
            //
            // linkLblHelp
            //
            this.linkLblHelp.AutoSize = true;
            this.linkLblHelp.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.linkLblHelp.Location = new System.Drawing.Point(508, 23);
            this.linkLblHelp.Name = "linkLblHelp";
            this.linkLblHelp.Size = new System.Drawing.Size(32, 15);
            this.linkLblHelp.TabIndex = 5;
            this.linkLblHelp.TabStop = true;
            this.linkLblHelp.Text = "Help";
            this.linkLblHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLblHelp_LinkClicked);
            //
            // MainForm
            //
            this.AcceptButton = this.btnRunScript;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(960, 650);
            this.Controls.Add(this.grpDrawings);
            this.Controls.Add(this.grpScript);
            this.Controls.Add(this.footerPanel);
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(860, 620);
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Batch Scripter";
            this.grpDrawings.ResumeLayout(false);
            this.grpScript.ResumeLayout(false);
            this.grpScript.PerformLayout();
            this.footerPanel.ResumeLayout(false);
            this.footerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Logo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GitHub)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LinkedIn)).EndInit();
            this.ResumeLayout(false);
        }
    }
}
