/*
* Twitter.cs
*
* Copyright © 2008 Patrick "Ricky" Smith <ricky@digitally-born.com>
*
* This file is part of the Twitterizer library
*
* The Twitterizer library is free software: you can redistribute it
* and/or modify it under the terms of the GNU General Public License as
* published by the Free Software Foundation, either version 3 of the
* License, or (at your option) any later version.
* 
* This program is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.
*
* You should have received a copy of the GNU General Public License
* along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/
namespace Twitterizer.Framework
{
    public class Twitter
    {
        public TwitterDirectMessageMethods DirectMessages;
        public TwitterStatusMethods Status;
        public TwitterUserMethods User;

        public Twitter(string UserName, string Password)
        {
            DirectMessages = new TwitterDirectMessageMethods(UserName, Password);
            Status = new TwitterStatusMethods(UserName, Password);
            User = new TwitterUserMethods(UserName, Password);
        }
    }
}
