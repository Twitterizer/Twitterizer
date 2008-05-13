using System;
using System.Collections;
using System.Text;

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
