namespace Twitterizer.TestApp
{
    using System;
    using System.Windows.Forms;

    public partial class ConfigureForm : Form
    {
        public ConfigureForm()
        {
            InitializeComponent();

            string[] configValues = XMLConfiguration.Load(Application.StartupPath);
            if (configValues != null)
            {
                UserNameTextBox.Text = configValues[0];
                PasswordTextBox.Text = configValues[1];
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (Twitterizer.Framework.Twitter.VerifyCredentials(UserNameTextBox.Text, PasswordTextBox.Text))
            {
                XMLConfiguration.Save(UserNameTextBox.Text, PasswordTextBox.Text, Application.StartupPath);

                ((MainForm)Application.OpenForms["MainForm"]).UserName = UserNameTextBox.Text;
                ((MainForm)Application.OpenForms["MainForm"]).Password = PasswordTextBox.Text;

                this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid Credentials", "Uh Oh!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ConfigureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.None:
                case CloseReason.UserClosing:
                    e.Cancel = true;
                    this.Hide();
                    break;
                default:
                    break;
            }
        }

        private void ConfigureForm_Load(object sender, EventArgs e)
        {
        }
    }
}