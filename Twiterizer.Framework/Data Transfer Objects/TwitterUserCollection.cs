/*
* TwitterCollection.cs
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
using System.Collections;

namespace Twitterizer.Framework
{
    public class TwitterUserCollection : CollectionBase
    {
        public TwitterUser this[int index]
        {
            get
            {
                return ((TwitterUser)List[index]);
            }
            set
            {
                List[index] = value;
            }
        }


        public int Add(TwitterUser value)
        {
            return (List.Add(value));
        }

        public int IndexOf(TwitterUser value)
        {
            return (List.IndexOf(value));
        }

        public void Insert(int index, TwitterUser value)
        {
            List.Insert(index, value);
        }

        public void Remove(TwitterUser value)
        {
            List.Remove(value);
        }

        public bool Contains(TwitterUser value)
        {
            return (List.Contains(value));
        }

    }
}
