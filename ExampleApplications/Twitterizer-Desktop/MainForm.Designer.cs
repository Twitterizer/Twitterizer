namespace Twitterizer_Desktop
{
    partial class MainForm
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
            this.WelcomeLabel = new System.Windows.Forms.Label();
            this.WelcomeTextPanel = new System.Windows.Forms.Panel();
            this.HomeTimelinePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.WelcomeTextPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // WelcomeLabel
            // 
            this.WelcomeLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WelcomeLabel.Location = new System.Drawing.Point(0, 0);
            this.WelcomeLabel.Name = "WelcomeLabel";
            this.WelcomeLabel.Size = new System.Drawing.Size(326, 89);
            this.WelcomeLabel.TabIndex = 0;
            this.WelcomeLabel.Text = "Welcome Text Here.";
            // 
            // WelcomeTextPanel
            // 
            this.WelcomeTextPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.WelcomeTextPanel.Controls.Add(this.WelcomeLabel);
            this.WelcomeTextPanel.Location = new System.Drawing.Point(12, 12);
            this.WelcomeTextPanel.Name = "WelcomeTextPanel";
            this.WelcomeTextPanel.Size = new System.Drawing.Size(328, 91);
            this.WelcomeTextPanel.TabIndex = 1;
            // 
            // HomeTimelinePanel
            // 
            this.HomeTimelinePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.HomeTimelinePanel.AutoScroll = true;
            this.HomeTimelinePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HomeTimelinePanel.Location = new System.Drawing.Point(12, 109);
            this.HomeTimelinePanel.Name = "HomeTimelinePanel";
            this.HomeTimelinePanel.Size = new System.Drawing.Size(327, 461);
            this.HomeTimelinePanel.TabIndex = 3;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 592);
            this.Controls.Add(this.HomeTimelinePanel);
            this.Controls.Add(this.WelcomeTextPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.WelcomeTextPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label WelcomeLabel;
        private System.Windows.Forms.Panel WelcomeTextPanel;
        private System.Windows.Forms.FlowLayoutPanel HomeTimelinePanel;
    }
}