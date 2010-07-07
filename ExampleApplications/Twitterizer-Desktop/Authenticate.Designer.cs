namespace Twitterizer_Desktop
{
    partial class Authenticate
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
            this.RequestLinkText = new System.Windows.Forms.LinkLabel();
            this.PinButton = new System.Windows.Forms.Button();
            this.PinTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // RequestLinkText
            // 
            this.RequestLinkText.AutoSize = true;
            this.RequestLinkText.Location = new System.Drawing.Point(13, 13);
            this.RequestLinkText.Name = "RequestLinkText";
            this.RequestLinkText.Size = new System.Drawing.Size(55, 13);
            this.RequestLinkText.TabIndex = 0;
            this.RequestLinkText.TabStop = true;
            this.RequestLinkText.Text = "linkLabel1";
            this.RequestLinkText.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.RequestLinkText_LinkClicked);
            // 
            // PinButton
            // 
            this.PinButton.Location = new System.Drawing.Point(122, 60);
            this.PinButton.Name = "PinButton";
            this.PinButton.Size = new System.Drawing.Size(105, 23);
            this.PinButton.TabIndex = 1;
            this.PinButton.Text = "Authenticate Me";
            this.PinButton.UseVisualStyleBackColor = true;
            this.PinButton.Click += new System.EventHandler(this.PinButton_Click);
            // 
            // PinTextBox
            // 
            this.PinTextBox.Location = new System.Drawing.Point(16, 60);
            this.PinTextBox.MaxLength = 7;
            this.PinTextBox.Name = "PinTextBox";
            this.PinTextBox.Size = new System.Drawing.Size(100, 20);
            this.PinTextBox.TabIndex = 2;
            // 
            // Authenticate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(523, 92);
            this.Controls.Add(this.PinTextBox);
            this.Controls.Add(this.PinButton);
            this.Controls.Add(this.RequestLinkText);
            this.Name = "Authenticate";
            this.Text = "Authenticate";
            this.Load += new System.EventHandler(this.Authenticate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.LinkLabel RequestLinkText;
        private System.Windows.Forms.Button PinButton;
        private System.Windows.Forms.TextBox PinTextBox;


    }
}