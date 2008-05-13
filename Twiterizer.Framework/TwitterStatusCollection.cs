using System;
using System.Collections;
using System.Text;

namespace Twitterizer.Framework
{
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
