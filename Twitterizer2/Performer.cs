using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Json;

namespace Twitterizer.Core
{
    public static class Performer<T>
    {
        public static T PerformAction(BaseCommand<T> Command)
        {
            Command.Init();
            Command.Validate();
            T result = Command.ExecuteCommand();

            return result;
        }
    }
}
