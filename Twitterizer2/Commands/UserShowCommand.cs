using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twitterizer.Commands
{
    public class UserShowCommand : Core.BaseCommand<User>
    {
        public Int64 UserID { get; set; }
        public string UserName { get; set; }
        public string UserIDOrName { get; set; }

        public UserShowCommand()
            : base("GET", "http://twitter.com/users/show.json")
        {

        }

        public UserShowCommand(string OAuthToken)
            : base("GET", "http://twitter.com/users/show.json", OAuthToken)
        {

        }

        public override void Init()
        {
            base.RequestParameters.Add("id", this.UserIDOrName);
            base.RequestParameters.Add("user_id", this.UserID.ToString());
            base.RequestParameters.Add("screen_name", this.UserName);
        }

        public override void Validate()
        {
            this.IsValid = (UserID > 0 || !string.IsNullOrEmpty(UserIDOrName) || !string.IsNullOrEmpty(UserName));
        }
    }
}
