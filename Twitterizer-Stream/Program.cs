using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Twitterizer;

namespace Twitterizer_Stream
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What is your username?");
            string username = Console.ReadLine();

            Console.WriteLine("What is your passsword?");
            string password = Console.ReadLine();

            for (int i = 0; i < 3; i++)
            {
                if (!string.IsNullOrEmpty(password))
                {
                    break;
                }

                Console.WriteLine("What is your passsword?");
                password = Console.ReadLine();
            }

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return;
            }

            Console.Clear();
            Console.WriteLine("Press any key to begin the stream. Press any key to stop.");
            Console.ReadKey();

            TwitterStream stream = new TwitterStream(username, password);
            stream.OnStatus += new TwitterStatusReceivedHandler(stream_OnStatus);

            stream.StartStream();

            Console.ReadKey();
        }

        static void stream_OnStatus(object sender, TwitterStatus status)
        {
            Console.WriteLine(string.Format("[{0:HH:mm:ss} {1}] {2}", status.CreatedDate, status.User.ScreenName, status.Text));
        }
    }
}
