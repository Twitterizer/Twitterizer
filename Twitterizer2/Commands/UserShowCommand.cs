using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twitter_2._0_Structure_Idea.Commands
{
    public class UserShowCommand : BaseCommand<User>
    {
        public Int64 UserID { get; set; }
        public string UserName { get; set; }
        public string UserIDOrName { get; set; }

        public UserShowCommand()
            : base("GET", "http://twitter.com/users/show.json")
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
