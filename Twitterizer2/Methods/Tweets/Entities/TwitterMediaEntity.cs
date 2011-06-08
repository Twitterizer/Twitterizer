using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twitterizer.Entities
{
    public class TwitterMediaEntity : TwitterEntity
    {
        public enum MediaTypes
        {
            Unknown,
            Photo
        }

        internal TwitterMediaEntity() { }

        public MediaTypes MediaType { get; set; }
        public decimal Id { get; set; }
        public string IdString { get; set; }
        public string MediaUrl { get; set; }
        public string MediaUrlSecure { get; set; }
        public string Url { get; set; }
        public string DisplayUrl { get; set; }
        public string ExpandedUrl { get; set; }
        public List<MediaSize> Sizes { get; set; }
        
        public class MediaSize
        {
            public enum MediaSizeResizes
            {
                Unknown,
                Crop,
                Fit
            }

            public enum MediaSizes
            {
                Unknown,
                Thumb,
                Small,
                Medium,
                Large
            }

            public MediaSizes Size { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public MediaSizeResizes Resize { get; set; }
        }
    }
}
