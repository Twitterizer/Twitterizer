using System;
using System.Collections.Generic;
using System.Text;

namespace Twitterizer.Framework
{
    public enum TwitterParameterNames
    {
        ID,
        Since,
        SinceID,
        Count,
        Page
    }

    public class TwitterParameters : Dictionary<TwitterParameterNames, object>
    {
        public string BuildActionUri(string Uri)
        {
            if (Count == 0)
                return Uri;

            string parameterString = string.Empty;

            foreach (TwitterParameterNames key in Keys)
            {
                switch (key)
                {
                    case TwitterParameterNames.Since:
                        parameterString = string.Format("{0}&since={1}", parameterString, this[key]);
                        break;
                    case TwitterParameterNames.SinceID:
                        parameterString = string.Format("{0}&since_id={1}", parameterString, this[key]);
                        break;
                    case TwitterParameterNames.Count:
                        parameterString = string.Format("{0}&count={1}", parameterString, this[key]);
                        break;
                    case TwitterParameterNames.Page:
                        parameterString = string.Format("{0}&page={1}", parameterString, this[key]);
                        break;
                }
            }

            if (string.IsNullOrEmpty(parameterString))
                return Uri;

            // First char of parameterString is a leading & that should be removed
            return string.Format("{0}?{1}", Uri, parameterString.Remove(0, 1));
        }

        public new void Add(TwitterParameterNames Key, object Value)
        {
            switch (Key)
            {
                case TwitterParameterNames.Since:
                    if (!(Value is DateTime))
                        throw new ApplicationException("Value given for since was not a Date.");

                    DateTime DateValue = (DateTime)Value;

                    // RFC1123 date string
                    base.Add(Key, DateValue.ToString("r"));

                    break;
                default:
                    base.Add(Key, Value.ToString());
                    break;
            }
        }
    }
}
