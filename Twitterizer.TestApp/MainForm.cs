using System;
using System.Windows.Forms;
using Twitterizer.Framework;

namespace Twitterizer.TestApp
{
    public partial class MainForm : Form
    {
        const string SRC = "twirssi"; //The Name of this Application (must be a registered source of Twitter)
        private readonly ConfigureForm ConfigFormSingleton = new ConfigureForm();

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

            Width = 541;
            Height = 209;

            // Shared twitter test account
            userName = "Twit_er_izer";
            password = "23uSWutr";
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
                    Width = 541;
                    Height = 209;
                    break;
                case "TimelineTabPage":
                    Width = 789;
                    Height = 599;
                    break;
                case "FriendsTabPage":
                    Width = 789;
                    Height = 599;
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
                Twitter t = new Twitter(userName, password, SRC);
                TwitterParameters Parameters = new TwitterParameters();
                Parameters.Add(TwitterParameterNames.Since, new DateTime(2008, 8, 1));
                TimelineDataGridView.DataSource = t.Status.FriendsTimeline(Parameters);
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
                Twitter t = new Twitter(userName, password, SRC);
                TimelineDataGridView.DataSource = t.Status.PublicTimeline();
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
                Twitter t = new Twitter(userName, password, SRC);
                TimelineDataGridView.DataSource = t.Status.UserTimeline();
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
                Twitter t = new Twitter(userName, password, SRC);
                t.Status.Update(UpdateTextBox.Text);
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
                Twitter t = new Twitter(userName, password, SRC);
                FriendsDataGridView.DataSource = t.User.Friends();
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
                Twitter t = new Twitter(userName, password, SRC);
                FriendsDataGridView.DataSource = t.User.Followers();
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

        private void directMessagesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                Twitter t = new Twitter(userName, password, SRC);
                TimelineDataGridView.DataSource = t.DirectMessages.DirectMessages(null);
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

        private void directMessagesSentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                Twitter t = new Twitter(userName, password, SRC);
                TimelineDataGridView.DataSource = t.DirectMessages.DirectMessagesSent(null);
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

		private void DirectMessageButton_Click(object sender, EventArgs e)
		{
			Cursor = Cursors.WaitCursor;

			try
			{
				Twitter t = new Twitter(userName, password, SRC);
				t.DirectMessages.New(DirectMessageUserTextBox.Text, UpdateTextBox.Text);
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

		private void btnVCSubmit_Click(object sender, EventArgs e)
		{
			try
			{
				lblVCMessage.Text = Twitter.VerifyCredentials(tbVCUsername.Text, tbVCPassword.Text).ToString();
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
    }
}