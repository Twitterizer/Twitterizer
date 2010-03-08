namespace Twitterizer_Desktop
{
    partial class TweetTimelineControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.userPictureBox = new System.Windows.Forms.PictureBox();
            this.UserNameLabel = new System.Windows.Forms.Label();
            this.TextLabel = new System.Windows.Forms.Label();
            this.DateLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.userPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // userPictureBox
            // 
            this.userPictureBox.Location = new System.Drawing.Point(1, 1);
            this.userPictureBox.Name = "userPictureBox";
            this.userPictureBox.Size = new System.Drawing.Size(64, 64);
            this.userPictureBox.TabIndex = 0;
            this.userPictureBox.TabStop = false;
            // 
            // UserNameLabel
            // 
            this.UserNameLabel.AutoSize = true;
            this.UserNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UserNameLabel.Location = new System.Drawing.Point(71, 4);
            this.UserNameLabel.Name = "UserNameLabel";
            this.UserNameLabel.Size = new System.Drawing.Size(83, 17);
            this.UserNameLabel.TabIndex = 1;
            this.UserNameLabel.Text = "UserName";
            // 
            // TextLabel
            // 
            this.TextLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TextLabel.Location = new System.Drawing.Point(74, 25);
            this.TextLabel.Name = "TextLabel";
            this.TextLabel.Size = new System.Drawing.Size(224, 39);
            this.TextLabel.TabIndex = 2;
            this.TextLabel.Text = "label1";
            // 
            // DateLabel
            // 
            this.DateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DateLabel.Location = new System.Drawing.Point(198, 6);
            this.DateLabel.Name = "DateLabel";
            this.DateLabel.Size = new System.Drawing.Size(100, 15);
            this.DateLabel.TabIndex = 3;
            this.DateLabel.Text = "Date";
            this.DateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // TweetTimelineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DateLabel);
            this.Controls.Add(this.TextLabel);
            this.Controls.Add(this.UserNameLabel);
            this.Controls.Add(this.userPictureBox);
            this.Name = "TweetTimelineControl";
            this.Size = new System.Drawing.Size(301, 76);
            ((System.ComponentModel.ISupportInitialize)(this.userPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox userPictureBox;
        private System.Windows.Forms.Label UserNameLabel;
        private System.Windows.Forms.Label TextLabel;
        private System.Windows.Forms.Label DateLabel;
    }
}
