using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Twitterizer
{
    [Serializable, DataContract(Name="user")]
    public class User : Core.BaseObject
    {
        [DataMember(Name = "id")]
        public Int64 ID { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "location")]
        public string Location { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="ID">The ID.</param>
        /// <returns></returns>
        public static User GetUser(string OAuthAccessToken, Int64 ID)
        {
            Commands.UserShowCommand command = new Commands.UserShowCommand(OAuthAccessToken);
            command.UserID = ID;

            return Core.Performer<User>.PerformAction(command);
        }
    }
}
