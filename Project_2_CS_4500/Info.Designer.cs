namespace Project_2_CS_4500
{
    partial class Info
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Info));
            this.label1 = new System.Windows.Forms.Label();
            this.panelInfo = new System.Windows.Forms.Panel();
            this.bRestart = new System.Windows.Forms.Button();
            this.bResume = new System.Windows.Forms.Button();
            this.tBoxInfo = new System.Windows.Forms.TextBox();
            this.bStart = new System.Windows.Forms.Button();
            this.panelInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.No;
            this.label1.Location = new System.Drawing.Point(50, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(748, 240);
            this.label1.TabIndex = 0;
            this.label1.Text = resources.GetString("label1.Text");
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // panelInfo
            // 
            this.panelInfo.Controls.Add(this.bStart);
            this.panelInfo.Controls.Add(this.bRestart);
            this.panelInfo.Controls.Add(this.bResume);
            this.panelInfo.Controls.Add(this.tBoxInfo);
            this.panelInfo.Location = new System.Drawing.Point(162, 325);
            this.panelInfo.Name = "panelInfo";
            this.panelInfo.Size = new System.Drawing.Size(453, 99);
            this.panelInfo.TabIndex = 1;
            // 
            // bRestart
            // 
            this.bRestart.Location = new System.Drawing.Point(236, 37);
            this.bRestart.Name = "bRestart";
            this.bRestart.Size = new System.Drawing.Size(71, 41);
            this.bRestart.TabIndex = 2;
            this.bRestart.Text = "Restart";
            this.bRestart.UseVisualStyleBackColor = true;
            this.bRestart.Click += new System.EventHandler(this.bRestart_Click);
            // 
            // bResume
            // 
            this.bResume.Location = new System.Drawing.Point(116, 37);
            this.bResume.Name = "bResume";
            this.bResume.Size = new System.Drawing.Size(71, 41);
            this.bResume.TabIndex = 1;
            this.bResume.Text = "Resume";
            this.bResume.UseVisualStyleBackColor = true;
            this.bResume.Click += new System.EventHandler(this.bResume_Click);
            // 
            // tBoxInfo
            // 
            this.tBoxInfo.Location = new System.Drawing.Point(3, 8);
            this.tBoxInfo.Name = "tBoxInfo";
            this.tBoxInfo.ReadOnly = true;
            this.tBoxInfo.Size = new System.Drawing.Size(447, 23);
            this.tBoxInfo.TabIndex = 0;
            // 
            // bStart
            // 
            this.bStart.Location = new System.Drawing.Point(343, 37);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(71, 41);
            this.bStart.TabIndex = 2;
            this.bStart.Text = "Get Started!";
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // Info
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelInfo);
            this.Controls.Add(this.label1);
            this.Name = "Info";
            this.Text = "Info";
            this.panelInfo.ResumeLayout(false);
            this.panelInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private Panel panelInfo;
        private Button bRestart;
        private Button bResume;
        private TextBox tBoxInfo;
        private Button bStart;
    }
}
