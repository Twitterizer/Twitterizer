//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="Patrick 'Ricky' Smith">
//  This file is part of the Twitterizer library (http://code.google.com/p/twitterizer/)
// 
//  Copyright (c) 2010, Patrick "Ricky" Smith (ricky@digitally-born.com)
//  All rights reserved.
//  
//  Redistribution and use in source and binary forms, with or without modification, are 
//  permitted provided that the following conditions are met:
// 
//  - Redistributions of source code must retain the above copyright notice, this list 
//    of conditions and the following disclaimer.
//  - Redistributions in binary form must reproduce the above copyright notice, this list 
//    of conditions and the following disclaimer in the documentation and/or other 
//    materials provided with the distribution.
//  - Neither the name of the Twitterizer nor the names of its contributors may be 
//    used to endorse or promote products derived from this software without specific 
//    prior written permission.
// 
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
//  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
//  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//  IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
//  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
//  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
//  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
//  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
//  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
//  POSSIBILITY OF SUCH DAMAGE.
// </copyright>
// <author>Ricky Smith</author>
// <summary>The program class of the stream example application</summary>
//-----------------------------------------------------------------------

namespace Twitterizer_Stream
{
    using System;
    using Twitterizer;

    /// <summary>
    /// The program class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// The program entry point
        /// </summary>
        /// <param name="args">The command line arguments.</param>
        protected static void Main(string[] args)
        {
            Console.WriteLine("What is your username?");
            string username = Console.ReadLine();

            Console.WriteLine("What is your passsword?");
            string password = Console.ReadLine();

            // Ask for the password up to 3 times.
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

            using (TwitterStream stream = new TwitterStream(username, password))
            {
                stream.OnStatus += new TwitterStatusReceivedHandler(Stream_OnStatus);

                stream.StartStream();

                Console.ReadKey();

                stream.EndStream();
            }
        }

        /// <summary>
        /// Handles the status received event.
        /// </summary>
        /// <param name="status">The status.</param>
        protected static void Stream_OnStatus(TwitterStatus status)
        {
            Console.WriteLine(string.Format("[{0:HH:mm:ss} {1}] {2}", status.CreatedDate, status.User.ScreenName, status.Text));
        }
    }
}
