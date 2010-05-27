using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Twitterizer.Entities
{
    public class TwitterHashTagEntity : TwitterEntity
    {
        internal TwitterHashTagEntity()
        { 
        }

        public string Text { get; set; }
    }
}
