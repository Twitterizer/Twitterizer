using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Twitterizer.Framework
{
    public class TwitterStatus
    {
        private DateTime created;
        public DateTime Created
        {
            get { return created; }
            set { created = value; }
        }

        private int id;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        private string text;
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        private string source;
        public string Source
        {
            get { return source; }
            set { source = value; }
        }

        private bool isTruncated;
        public bool IsTruncated
        {
            get { return isTruncated; }
            set { isTruncated = value; }
        }

        private bool isFavorited;
        public bool IsFavorited
        {
            get { return isFavorited; }
            set { isFavorited = value; }
        }

        private TwitterUser twitterUser;
        public TwitterUser TwitterUser
        {
            get { return twitterUser; }
            set { twitterUser = value; }
        }
    }

    public class TwitterStatusCollection : CollectionBase
    {
        public TwitterStatus this[int index]
        {
            get
            {
                return ((TwitterStatus)List[index]);
            }
            set
            {
                List[index] = value;
            }
        }


        public int Add(TwitterStatus value)
        {
            return (List.Add(value));
        }

        public int IndexOf(TwitterStatus value)
        {
            return (List.IndexOf(value));
        }

        public void Insert(int index, TwitterStatus value)
        {
            List.Insert(index, value);
        }

        public void Remove(TwitterStatus value)
        {
            List.Remove(value);
        }

        public bool Contains(TwitterStatus value)
        {
            // If value is not of type Int16, this will return false.
            return (List.Contains(value));
        }


    }
}
