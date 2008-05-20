using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Twitterizer.TestApp
{
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
            XMLConfiguration.Save(UserNameTextBox.Text, PasswordTextBox.Text, Application.StartupPath);

            ((MainForm)Application.OpenForms["MainForm"]).UserName = UserNameTextBox.Text;
            ((MainForm)Application.OpenForms["MainForm"]).Password = PasswordTextBox.Text;

            this.Close();
        }
    }
}