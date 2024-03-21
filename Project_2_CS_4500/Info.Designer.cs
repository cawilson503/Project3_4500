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
            label1 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AccessibleRole = AccessibleRole.None;
            label1.AutoSize = true;
            label1.Cursor = Cursors.No;
            label1.Location = new Point(50, 59);
            label1.Name = "label1";
            label1.Size = new Size(662, 195);
            label1.TabIndex = 0;
            label1.Text = resources.GetString("label1.Text");
            // 
            // Info
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Name = "Info";
            Text = "Info";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
    }
}
