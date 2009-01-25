namespace Twitterizer.TestApp
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
			this.components = new System.ComponentModel.Container();
			this.UpdateButton = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.UpdateTextBox = new System.Windows.Forms.TextBox();
			this.CharCountLabel = new System.Windows.Forms.Label();
			this.MainFormTabControl = new System.Windows.Forms.TabControl();
			this.UpdateTabPage = new System.Windows.Forms.TabPage();
			this.DirectMessageButton = new System.Windows.Forms.Button();
			this.FriendsTabPage = new System.Windows.Forms.TabPage();
			this.FriendsDataGridView = new System.Windows.Forms.DataGridView();
			this.CurrentStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.TimelineTabPage = new System.Windows.Forms.TabPage();
			this.TimelineDataGridView = new System.Windows.Forms.DataGridView();
			this.IsFavorited = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.TimelinesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.friendsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.publicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.userToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.directMessagesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.directMessagesSentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.configureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.friendsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.friendsToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.followersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.directMessagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.label2 = new System.Windows.Forms.Label();
			this.DirectMessageUserTextBox = new System.Windows.Forms.TextBox();
			this.userNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.screenNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.locationDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.profileImageUriDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewLinkColumn();
			this.profileUriDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewLinkColumn();
			this.isProtectedDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.numberOfFollowersDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.twitterUserCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.twitterUserDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.textDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.createdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.twitterStatusCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.MainFormTabControl.SuspendLayout();
			this.UpdateTabPage.SuspendLayout();
			this.FriendsTabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.FriendsDataGridView)).BeginInit();
			this.TimelineTabPage.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.TimelineDataGridView)).BeginInit();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.twitterUserCollectionBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.twitterStatusCollectionBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// UpdateButton
			// 
			this.UpdateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.UpdateButton.Enabled = false;
			this.UpdateButton.Location = new System.Drawing.Point(727, 477);
			this.UpdateButton.Name = "UpdateButton";
			this.UpdateButton.Size = new System.Drawing.Size(75, 23);
			this.UpdateButton.TabIndex = 1;
			this.UpdateButton.Text = "Update";
			this.UpdateButton.UseVisualStyleBackColor = true;
			this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(3, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(106, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "What are you doing?";
			// 
			// UpdateTextBox
			// 
			this.UpdateTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.UpdateTextBox.Location = new System.Drawing.Point(6, 19);
			this.UpdateTextBox.MaxLength = 140;
			this.UpdateTextBox.Multiline = true;
			this.UpdateTextBox.Name = "UpdateTextBox";
			this.UpdateTextBox.Size = new System.Drawing.Size(796, 452);
			this.UpdateTextBox.TabIndex = 3;
			this.UpdateTextBox.TextChanged += new System.EventHandler(this.UpdateTextBox_TextChanged);
			// 
			// CharCountLabel
			// 
			this.CharCountLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.CharCountLabel.AutoSize = true;
			this.CharCountLabel.Location = new System.Drawing.Point(6, 482);
			this.CharCountLabel.Name = "CharCountLabel";
			this.CharCountLabel.Size = new System.Drawing.Size(71, 13);
			this.CharCountLabel.TabIndex = 4;
			this.CharCountLabel.Text = "140 chars left";
			// 
			// MainFormTabControl
			// 
			this.MainFormTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.MainFormTabControl.Controls.Add(this.UpdateTabPage);
			this.MainFormTabControl.Controls.Add(this.FriendsTabPage);
			this.MainFormTabControl.Controls.Add(this.TimelineTabPage);
			this.MainFormTabControl.Location = new System.Drawing.Point(12, 27);
			this.MainFormTabControl.Name = "MainFormTabControl";
			this.MainFormTabControl.SelectedIndex = 0;
			this.MainFormTabControl.Size = new System.Drawing.Size(816, 532);
			this.MainFormTabControl.TabIndex = 5;
			this.MainFormTabControl.SelectedIndexChanged += new System.EventHandler(this.MainFormTabControl_SelectedIndexChanged);
			// 
			// UpdateTabPage
			// 
			this.UpdateTabPage.Controls.Add(this.DirectMessageUserTextBox);
			this.UpdateTabPage.Controls.Add(this.label2);
			this.UpdateTabPage.Controls.Add(this.DirectMessageButton);
			this.UpdateTabPage.Controls.Add(this.UpdateTextBox);
			this.UpdateTabPage.Controls.Add(this.label1);
			this.UpdateTabPage.Controls.Add(this.CharCountLabel);
			this.UpdateTabPage.Controls.Add(this.UpdateButton);
			this.UpdateTabPage.Location = new System.Drawing.Point(4, 22);
			this.UpdateTabPage.Name = "UpdateTabPage";
			this.UpdateTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.UpdateTabPage.Size = new System.Drawing.Size(808, 506);
			this.UpdateTabPage.TabIndex = 0;
			this.UpdateTabPage.Text = "Update";
			this.UpdateTabPage.UseVisualStyleBackColor = true;
			// 
			// DirectMessageButton
			// 
			this.DirectMessageButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.DirectMessageButton.Location = new System.Drawing.Point(622, 477);
			this.DirectMessageButton.Name = "DirectMessageButton";
			this.DirectMessageButton.Size = new System.Drawing.Size(99, 23);
			this.DirectMessageButton.TabIndex = 5;
			this.DirectMessageButton.Text = "Direct Message";
			this.DirectMessageButton.UseVisualStyleBackColor = true;
			this.DirectMessageButton.Click += new System.EventHandler(this.DirectMessageButton_Click);
			// 
			// FriendsTabPage
			// 
			this.FriendsTabPage.Controls.Add(this.FriendsDataGridView);
			this.FriendsTabPage.Location = new System.Drawing.Point(4, 22);
			this.FriendsTabPage.Name = "FriendsTabPage";
			this.FriendsTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.FriendsTabPage.Size = new System.Drawing.Size(808, 506);
			this.FriendsTabPage.TabIndex = 2;
			this.FriendsTabPage.Text = "Friends";
			this.FriendsTabPage.UseVisualStyleBackColor = true;
			// 
			// FriendsDataGridView
			// 
			this.FriendsDataGridView.AllowUserToAddRows = false;
			this.FriendsDataGridView.AllowUserToDeleteRows = false;
			this.FriendsDataGridView.AutoGenerateColumns = false;
			this.FriendsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.FriendsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.userNameDataGridViewTextBoxColumn,
            this.screenNameDataGridViewTextBoxColumn,
            this.locationDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn,
            this.profileImageUriDataGridViewTextBoxColumn,
            this.profileUriDataGridViewTextBoxColumn,
            this.isProtectedDataGridViewCheckBoxColumn,
            this.numberOfFollowersDataGridViewTextBoxColumn,
            this.CurrentStatus});
			this.FriendsDataGridView.DataSource = this.twitterUserCollectionBindingSource;
			this.FriendsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FriendsDataGridView.Location = new System.Drawing.Point(3, 3);
			this.FriendsDataGridView.Name = "FriendsDataGridView";
			this.FriendsDataGridView.ReadOnly = true;
			this.FriendsDataGridView.RowHeadersVisible = false;
			this.FriendsDataGridView.Size = new System.Drawing.Size(802, 500);
			this.FriendsDataGridView.TabIndex = 0;
			// 
			// CurrentStatus
			// 
			this.CurrentStatus.DataPropertyName = "Status.Text";
			this.CurrentStatus.HeaderText = "Current Status";
			this.CurrentStatus.Name = "CurrentStatus";
			this.CurrentStatus.ReadOnly = true;
			// 
			// TimelineTabPage
			// 
			this.TimelineTabPage.Controls.Add(this.TimelineDataGridView);
			this.TimelineTabPage.Location = new System.Drawing.Point(4, 22);
			this.TimelineTabPage.Name = "TimelineTabPage";
			this.TimelineTabPage.Padding = new System.Windows.Forms.Padding(3);
			this.TimelineTabPage.Size = new System.Drawing.Size(808, 506);
			this.TimelineTabPage.TabIndex = 1;
			this.TimelineTabPage.Text = "Timeline";
			this.TimelineTabPage.UseVisualStyleBackColor = true;
			// 
			// TimelineDataGridView
			// 
			this.TimelineDataGridView.AllowUserToAddRows = false;
			this.TimelineDataGridView.AllowUserToDeleteRows = false;
			this.TimelineDataGridView.AutoGenerateColumns = false;
			this.TimelineDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.TimelineDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IsFavorited,
            this.twitterUserDataGridViewTextBoxColumn,
            this.textDataGridViewTextBoxColumn,
            this.createdDataGridViewTextBoxColumn});
			this.TimelineDataGridView.DataSource = this.twitterStatusCollectionBindingSource;
			this.TimelineDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TimelineDataGridView.Location = new System.Drawing.Point(3, 3);
			this.TimelineDataGridView.Name = "TimelineDataGridView";
			this.TimelineDataGridView.ReadOnly = true;
			this.TimelineDataGridView.RowHeadersVisible = false;
			this.TimelineDataGridView.Size = new System.Drawing.Size(802, 500);
			this.TimelineDataGridView.TabIndex = 0;
			// 
			// IsFavorited
			// 
			this.IsFavorited.DataPropertyName = "IsFavorited";
			this.IsFavorited.HeaderText = "Fav?";
			this.IsFavorited.Name = "IsFavorited";
			this.IsFavorited.ReadOnly = true;
			// 
			// TimelinesToolStripMenuItem
			// 
			this.TimelinesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.friendsToolStripMenuItem,
            this.publicToolStripMenuItem,
            this.userToolStripMenuItem,
            this.directMessagesToolStripMenuItem1,
            this.directMessagesSentToolStripMenuItem});
			this.TimelinesToolStripMenuItem.Name = "TimelinesToolStripMenuItem";
			this.TimelinesToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
			this.TimelinesToolStripMenuItem.Text = "Timelines";
			// 
			// friendsToolStripMenuItem
			// 
			this.friendsToolStripMenuItem.Name = "friendsToolStripMenuItem";
			this.friendsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
			this.friendsToolStripMenuItem.Text = "Friends";
			this.friendsToolStripMenuItem.Click += new System.EventHandler(this.friendsToolStripMenuItem_Click);
			// 
			// publicToolStripMenuItem
			// 
			this.publicToolStripMenuItem.Name = "publicToolStripMenuItem";
			this.publicToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
			this.publicToolStripMenuItem.Text = "Public";
			this.publicToolStripMenuItem.Click += new System.EventHandler(this.publicToolStripMenuItem_Click);
			// 
			// userToolStripMenuItem
			// 
			this.userToolStripMenuItem.Name = "userToolStripMenuItem";
			this.userToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
			this.userToolStripMenuItem.Text = "User";
			this.userToolStripMenuItem.Click += new System.EventHandler(this.userToolStripMenuItem_Click);
			// 
			// directMessagesToolStripMenuItem1
			// 
			this.directMessagesToolStripMenuItem1.Name = "directMessagesToolStripMenuItem1";
			this.directMessagesToolStripMenuItem1.Size = new System.Drawing.Size(182, 22);
			this.directMessagesToolStripMenuItem1.Text = "DirectMessages";
			this.directMessagesToolStripMenuItem1.Click += new System.EventHandler(this.directMessagesToolStripMenuItem1_Click);
			// 
			// directMessagesSentToolStripMenuItem
			// 
			this.directMessagesSentToolStripMenuItem.Name = "directMessagesSentToolStripMenuItem";
			this.directMessagesSentToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
			this.directMessagesSentToolStripMenuItem.Text = "DirectMessagesSent";
			this.directMessagesSentToolStripMenuItem.Click += new System.EventHandler(this.directMessagesSentToolStripMenuItem_Click);
			// 
			// configureToolStripMenuItem
			// 
			this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
			this.configureToolStripMenuItem.Size = new System.Drawing.Size(66, 20);
			this.configureToolStripMenuItem.Text = "Configure";
			this.configureToolStripMenuItem.Click += new System.EventHandler(this.configureToolStripMenuItem_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.friendsToolStripMenuItem1,
            this.TimelinesToolStripMenuItem,
            this.configureToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(840, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// friendsToolStripMenuItem1
			// 
			this.friendsToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.friendsToolStripMenuItem2,
            this.followersToolStripMenuItem,
            this.directMessagesToolStripMenuItem});
			this.friendsToolStripMenuItem1.Name = "friendsToolStripMenuItem1";
			this.friendsToolStripMenuItem1.Size = new System.Drawing.Size(54, 20);
			this.friendsToolStripMenuItem1.Text = "Friends";
			// 
			// friendsToolStripMenuItem2
			// 
			this.friendsToolStripMenuItem2.Name = "friendsToolStripMenuItem2";
			this.friendsToolStripMenuItem2.Size = new System.Drawing.Size(130, 22);
			this.friendsToolStripMenuItem2.Text = "Friends";
			this.friendsToolStripMenuItem2.Click += new System.EventHandler(this.friendsToolStripMenuItem2_Click);
			// 
			// followersToolStripMenuItem
			// 
			this.followersToolStripMenuItem.Name = "followersToolStripMenuItem";
			this.followersToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
			this.followersToolStripMenuItem.Text = "Followers";
			this.followersToolStripMenuItem.Click += new System.EventHandler(this.followersToolStripMenuItem_Click);
			// 
			// directMessagesToolStripMenuItem
			// 
			this.directMessagesToolStripMenuItem.Name = "directMessagesToolStripMenuItem";
			this.directMessagesToolStripMenuItem.Size = new System.Drawing.Size(130, 22);
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(392, 483);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(118, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Send direct message to";
			// 
			// DirectMessageUserTextBox
			// 
			this.DirectMessageUserTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.DirectMessageUserTextBox.Location = new System.Drawing.Point(516, 480);
			this.DirectMessageUserTextBox.Name = "DirectMessageUserTextBox";
			this.DirectMessageUserTextBox.Size = new System.Drawing.Size(100, 20);
			this.DirectMessageUserTextBox.TabIndex = 7;
			// 
			// userNameDataGridViewTextBoxColumn
			// 
			this.userNameDataGridViewTextBoxColumn.DataPropertyName = "UserName";
			this.userNameDataGridViewTextBoxColumn.HeaderText = "UserName";
			this.userNameDataGridViewTextBoxColumn.Name = "userNameDataGridViewTextBoxColumn";
			this.userNameDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// screenNameDataGridViewTextBoxColumn
			// 
			this.screenNameDataGridViewTextBoxColumn.DataPropertyName = "ScreenName";
			this.screenNameDataGridViewTextBoxColumn.HeaderText = "ScreenName";
			this.screenNameDataGridViewTextBoxColumn.Name = "screenNameDataGridViewTextBoxColumn";
			this.screenNameDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// locationDataGridViewTextBoxColumn
			// 
			this.locationDataGridViewTextBoxColumn.DataPropertyName = "Location";
			this.locationDataGridViewTextBoxColumn.HeaderText = "Location";
			this.locationDataGridViewTextBoxColumn.Name = "locationDataGridViewTextBoxColumn";
			this.locationDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// descriptionDataGridViewTextBoxColumn
			// 
			this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
			this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
			this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
			this.descriptionDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// profileImageUriDataGridViewTextBoxColumn
			// 
			this.profileImageUriDataGridViewTextBoxColumn.DataPropertyName = "ProfileImageUri";
			this.profileImageUriDataGridViewTextBoxColumn.HeaderText = "ProfileImageUri";
			this.profileImageUriDataGridViewTextBoxColumn.Name = "profileImageUriDataGridViewTextBoxColumn";
			this.profileImageUriDataGridViewTextBoxColumn.ReadOnly = true;
			this.profileImageUriDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.profileImageUriDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// profileUriDataGridViewTextBoxColumn
			// 
			this.profileUriDataGridViewTextBoxColumn.DataPropertyName = "ProfileUri";
			this.profileUriDataGridViewTextBoxColumn.HeaderText = "ProfileUri";
			this.profileUriDataGridViewTextBoxColumn.Name = "profileUriDataGridViewTextBoxColumn";
			this.profileUriDataGridViewTextBoxColumn.ReadOnly = true;
			this.profileUriDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.profileUriDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			// 
			// isProtectedDataGridViewCheckBoxColumn
			// 
			this.isProtectedDataGridViewCheckBoxColumn.DataPropertyName = "IsProtected";
			this.isProtectedDataGridViewCheckBoxColumn.HeaderText = "IsProtected";
			this.isProtectedDataGridViewCheckBoxColumn.Name = "isProtectedDataGridViewCheckBoxColumn";
			this.isProtectedDataGridViewCheckBoxColumn.ReadOnly = true;
			// 
			// numberOfFollowersDataGridViewTextBoxColumn
			// 
			this.numberOfFollowersDataGridViewTextBoxColumn.DataPropertyName = "NumberOfFollowers";
			this.numberOfFollowersDataGridViewTextBoxColumn.HeaderText = "NumberOfFollowers";
			this.numberOfFollowersDataGridViewTextBoxColumn.Name = "numberOfFollowersDataGridViewTextBoxColumn";
			this.numberOfFollowersDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// twitterUserCollectionBindingSource
			// 
			this.twitterUserCollectionBindingSource.DataSource = typeof(Twitterizer.Framework.TwitterUserCollection);
			// 
			// twitterUserDataGridViewTextBoxColumn
			// 
			this.twitterUserDataGridViewTextBoxColumn.DataPropertyName = "TwitterUser";
			this.twitterUserDataGridViewTextBoxColumn.HeaderText = "TwitterUser";
			this.twitterUserDataGridViewTextBoxColumn.Name = "twitterUserDataGridViewTextBoxColumn";
			this.twitterUserDataGridViewTextBoxColumn.ReadOnly = true;
			this.twitterUserDataGridViewTextBoxColumn.Width = 125;
			// 
			// textDataGridViewTextBoxColumn
			// 
			this.textDataGridViewTextBoxColumn.DataPropertyName = "Text";
			this.textDataGridViewTextBoxColumn.HeaderText = "Text";
			this.textDataGridViewTextBoxColumn.Name = "textDataGridViewTextBoxColumn";
			this.textDataGridViewTextBoxColumn.ReadOnly = true;
			this.textDataGridViewTextBoxColumn.Width = 450;
			// 
			// createdDataGridViewTextBoxColumn
			// 
			this.createdDataGridViewTextBoxColumn.DataPropertyName = "Created";
			this.createdDataGridViewTextBoxColumn.HeaderText = "Created";
			this.createdDataGridViewTextBoxColumn.Name = "createdDataGridViewTextBoxColumn";
			this.createdDataGridViewTextBoxColumn.ReadOnly = true;
			this.createdDataGridViewTextBoxColumn.Width = 125;
			// 
			// twitterStatusCollectionBindingSource
			// 
			this.twitterStatusCollectionBindingSource.DataSource = typeof(Twitterizer.Framework.TwitterStatusCollection);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(840, 571);
			this.Controls.Add(this.MainFormTabControl);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.MainFormTabControl.ResumeLayout(false);
			this.UpdateTabPage.ResumeLayout(false);
			this.UpdateTabPage.PerformLayout();
			this.FriendsTabPage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.FriendsDataGridView)).EndInit();
			this.TimelineTabPage.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.TimelineDataGridView)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.twitterUserCollectionBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.twitterStatusCollectionBindingSource)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button UpdateButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox UpdateTextBox;
        private System.Windows.Forms.Label CharCountLabel;
        private System.Windows.Forms.TabControl MainFormTabControl;
        private System.Windows.Forms.TabPage UpdateTabPage;
        private System.Windows.Forms.TabPage TimelineTabPage;
        private System.Windows.Forms.ToolStripMenuItem TimelinesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem friendsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.DataGridView TimelineDataGridView;
        private System.Windows.Forms.BindingSource twitterStatusCollectionBindingSource;
        private System.Windows.Forms.ToolStripMenuItem publicToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem userToolStripMenuItem;
        private System.Windows.Forms.TabPage FriendsTabPage;
        private System.Windows.Forms.DataGridView FriendsDataGridView;
        private System.Windows.Forms.BindingSource twitterUserCollectionBindingSource;
        private System.Windows.Forms.ToolStripMenuItem friendsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem friendsToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem followersToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn userNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn screenNameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn locationDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewLinkColumn profileImageUriDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewLinkColumn profileUriDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn isProtectedDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numberOfFollowersDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurrentStatus;
        private System.Windows.Forms.ToolStripMenuItem directMessagesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem directMessagesToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem directMessagesSentToolStripMenuItem;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsFavorited;
        private System.Windows.Forms.DataGridViewTextBoxColumn twitterUserDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn textDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn createdDataGridViewTextBoxColumn;
		private System.Windows.Forms.Button DirectMessageButton;
		private System.Windows.Forms.TextBox DirectMessageUserTextBox;
		private System.Windows.Forms.Label label2;
    }
}