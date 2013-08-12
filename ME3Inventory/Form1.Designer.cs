namespace ME3Inventory
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnGo = new System.Windows.Forms.Button();
            this.txtOutput = new System.Windows.Forms.TextBox();
            this.btnArchive = new System.Windows.Forms.Button();
            this.lstArchive = new System.Windows.Forms.ListBox();
            this.radShowChanges = new System.Windows.Forms.RadioButton();
            this.radShowAll = new System.Windows.Forms.RadioButton();
            this.txtName = new System.Windows.Forms.TextBox();
            this.cmbPlatform = new System.Windows.Forms.ComboBox();
            this.radShowTotals = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // btnGo
            // 
            this.btnGo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnGo.Location = new System.Drawing.Point(667, 12);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(90, 27);
            this.btnGo.TabIndex = 0;
            this.btnGo.Text = "Pull New";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // txtOutput
            // 
            this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOutput.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutput.HideSelection = false;
            this.txtOutput.Location = new System.Drawing.Point(176, 45);
            this.txtOutput.Multiline = true;
            this.txtOutput.Name = "txtOutput";
            this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtOutput.Size = new System.Drawing.Size(581, 382);
            this.txtOutput.TabIndex = 1;
            this.txtOutput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOutput_KeyDown);
            // 
            // btnArchive
            // 
            this.btnArchive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnArchive.Location = new System.Drawing.Point(571, 12);
            this.btnArchive.Name = "btnArchive";
            this.btnArchive.Size = new System.Drawing.Size(90, 27);
            this.btnArchive.TabIndex = 2;
            this.btnArchive.Text = "Open Archive";
            this.btnArchive.UseVisualStyleBackColor = true;
            this.btnArchive.Click += new System.EventHandler(this.btnArchive_Click);
            // 
            // lstArchive
            // 
            this.lstArchive.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstArchive.FormattingEnabled = true;
            this.lstArchive.Location = new System.Drawing.Point(12, 45);
            this.lstArchive.Name = "lstArchive";
            this.lstArchive.Size = new System.Drawing.Size(158, 381);
            this.lstArchive.TabIndex = 3;
            this.lstArchive.SelectedIndexChanged += new System.EventHandler(this.lstArchive_SelectedIndexChanged);
            // 
            // radShowChanges
            // 
            this.radShowChanges.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radShowChanges.AutoSize = true;
            this.radShowChanges.Checked = true;
            this.radShowChanges.Location = new System.Drawing.Point(306, 17);
            this.radShowChanges.Name = "radShowChanges";
            this.radShowChanges.Size = new System.Drawing.Size(97, 17);
            this.radShowChanges.TabIndex = 4;
            this.radShowChanges.TabStop = true;
            this.radShowChanges.Text = "Show Changes";
            this.radShowChanges.UseVisualStyleBackColor = true;
            this.radShowChanges.CheckedChanged += new System.EventHandler(this.radShow_CheckedChanged);
            // 
            // radShowAll
            // 
            this.radShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radShowAll.AutoSize = true;
            this.radShowAll.Location = new System.Drawing.Point(499, 17);
            this.radShowAll.Name = "radShowAll";
            this.radShowAll.Size = new System.Drawing.Size(66, 17);
            this.radShowAll.TabIndex = 5;
            this.radShowAll.Text = "Show All";
            this.radShowAll.UseVisualStyleBackColor = true;
            this.radShowAll.CheckedChanged += new System.EventHandler(this.radShow_CheckedChanged);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(12, 16);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(158, 20);
            this.txtName.TabIndex = 6;
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
            // 
            // cmbPlatform
            // 
            this.cmbPlatform.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlatform.FormattingEnabled = true;
            this.cmbPlatform.Items.AddRange(new object[] {
            "PC",
            "Xbox",
            "PS3",
            "WiiU"});
            this.cmbPlatform.Location = new System.Drawing.Point(176, 16);
            this.cmbPlatform.MaxDropDownItems = 4;
            this.cmbPlatform.Name = "cmbPlatform";
            this.cmbPlatform.Size = new System.Drawing.Size(117, 21);
            this.cmbPlatform.TabIndex = 7;
            this.cmbPlatform.SelectionChangeCommitted += new System.EventHandler(this.cmbPlatform_SelectionChangeCommitted);
            // 
            // radShowTotals
            // 
            this.radShowTotals.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.radShowTotals.AutoSize = true;
            this.radShowTotals.Location = new System.Drawing.Point(409, 17);
            this.radShowTotals.Name = "radShowTotals";
            this.radShowTotals.Size = new System.Drawing.Size(84, 17);
            this.radShowTotals.TabIndex = 8;
            this.radShowTotals.TabStop = true;
            this.radShowTotals.Text = "Show Totals";
            this.radShowTotals.UseVisualStyleBackColor = true;
            this.radShowTotals.CheckedChanged += new System.EventHandler(this.radShow_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 439);
            this.Controls.Add(this.radShowTotals);
            this.Controls.Add(this.cmbPlatform);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.radShowAll);
            this.Controls.Add(this.radShowChanges);
            this.Controls.Add(this.lstArchive);
            this.Controls.Add(this.btnArchive);
            this.Controls.Add(this.txtOutput);
            this.Controls.Add(this.btnGo);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Mass Effect 3 Inventory";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnGo;
        private System.Windows.Forms.TextBox txtOutput;
        private System.Windows.Forms.Button btnArchive;
        private System.Windows.Forms.ListBox lstArchive;
        private System.Windows.Forms.RadioButton radShowChanges;
        private System.Windows.Forms.RadioButton radShowAll;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.ComboBox cmbPlatform;
        private System.Windows.Forms.RadioButton radShowTotals;
    }
}

