using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Twitterizer.Framework;

namespace Twitterizer.TestApp
{
    public partial class MainForm : Form
    {
        private ConfigureForm ConfigFormSingleton = new ConfigureForm();

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public MainForm()
        {
            InitializeComponent();

            this.Width = 541;
            this.Height = 209;

            string[] config = XMLConfiguration.Load(Application.StartupPath);
            if (config != null)
            {
                userName = config[0];
                password = config[1];
            }
            else
            {
                ConfigFormSingleton.Show();
            }
        }

        private void UpdateTextBox_TextChanged(object sender, EventArgs e)
        {
            CharCountLabel.Text =
                string.Format("{0} chars left",
                140 - UpdateTextBox.Text.Length);

            UpdateButton.Enabled = (UpdateTextBox.Text.Length <= 140);
        }

        private void configureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigFormSingleton.Show();
        }

        private void MainFormTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (MainFormTabControl.SelectedTab.Name)
            {
                case "UpdateTabPage":
                    this.Width = 541;
                    this.Height = 209;
                    break;
                case "TimelineTabPage":
                    this.Width = 789;
                    this.Height = 599;
                    break;
                case "FriendsTabPage":
                    this.Width = 789;
                    this.Height = 599;
                    break;
                default:
                    break;
            }
        }

        #region Timeline Menu Clicks
        private void friendsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                Twitterizer.Framework.Twitter t = new Twitterizer.Framework.Twitter(userName, password);
                TimelineDataGridView.DataSource = t.FriendsTimeline();
            }
            catch (TwitterizerException ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }

            MainFormTabControl.SelectedIndex = 2;
        }

        private void publicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                Twitterizer.Framework.Twitter t = new Twitterizer.Framework.Twitter(userName, password);
                TimelineDataGridView.DataSource = t.PublicTimeline();
            }
            catch (TwitterizerException ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            MainFormTabControl.SelectedIndex = 2;
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                Twitterizer.Framework.Twitter t = new Twitterizer.Framework.Twitter(userName, password);
                TimelineDataGridView.DataSource = t.UserTimeline();
                MainFormTabControl.SelectedIndex = 2;
            }
            catch (TwitterizerException ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }

        }
        #endregion

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;

            try
            {
                Twitterizer.Framework.Twitter t = new Twitterizer.Framework.Twitter(userName, password);
                t.Update(UpdateTextBox.Text);
            }
            catch (TwitterizerException ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #region Friends and Followers
        
        private void friendsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                Twitterizer.Framework.Twitter t = new Twitterizer.Framework.Twitter(userName, password);
                FriendsDataGridView.DataSource = t.Friends();
                MainFormTabControl.SelectedIndex = 1;
            }
            catch (TwitterizerException ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }
        private void followersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                Twitterizer.Framework.Twitter t = new Twitterizer.Framework.Twitter(userName, password);
                FriendsDataGridView.DataSource = t.Followers();
                MainFormTabControl.SelectedIndex = 1;
            }
            catch (TwitterizerException ex)
            {
                MessageBox.Show(this, ex.Message, "Error");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion
    }
}