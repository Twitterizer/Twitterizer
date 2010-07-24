namespace TwitterizerDesktop2
{
    using System;
    using System.Diagnostics;
    using System.Net;
    using System.Windows;
    using Twitterizer;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string ConsumerKey = "GoNLHcoS2tkG0rJNBgwMfg";
        private string ConsumerSecret = "9j4hqpKxntK6IbrrsG1RX69XzU3RssJE5rDKtWq9g";

        public static OAuthTokenResponse accessTokenResponse;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the button1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // Create a localhost address with a random port
            string listenerAddress = string.Format("http://localhost:{0}/", new Random().Next(1000, 10000));
            
            // Setup the httplistener class
            HttpListener listener = new HttpListener();
            listener.Prefixes.Add(listenerAddress);
            listener.Start();

            // Set our callback method
            IAsyncResult asyncResult = listener.BeginGetContext(HttpListener_Callback, listener);

            // Get the request token
            string requestToken = OAuthUtility.GetRequestToken(ConsumerKey, ConsumerSecret, listenerAddress).Token;

            // Send the user to Twitter to login and grant access
            Process.Start(new ProcessStartInfo()
            {
                FileName = OAuthUtility.BuildAuthorizationUri(requestToken).AbsoluteUri
            });

            // Wait for an event
            asyncResult.AsyncWaitHandle.WaitOne();

            // Put the application in a loop to wait for the async process to *really* finish processing
            DateTime timeout = DateTime.Now.AddMinutes(2);
            while (!asyncResult.IsCompleted || TokenHolder.Instance.AccessTokenResponse == null)
            {
                if (DateTime.Now > timeout)
                    return;
            }

            // Display the group box
            groupBox1.Visibility = System.Windows.Visibility.Visible;

            // Show the token values
            AccessTokenTextBlock.Text = string.Format(
                "{0} / {1}",
                TokenHolder.Instance.AccessTokenResponse.Token,
                TokenHolder.Instance.AccessTokenResponse.TokenSecret);

            // Show the user values
            UserTextBlock.Text = string.Format(
                "{0} - {1}",
                TokenHolder.Instance.AccessTokenResponse.UserId,
                TokenHolder.Instance.AccessTokenResponse.ScreenName);

            // Try to bring the window to the foreground
            this.Activate();
        }

        protected void HttpListener_Callback(IAsyncResult result)
        {
            HttpListener listener = (HttpListener)result.AsyncState;

            // Call EndGetContext to complete the asynchronous operation.
            HttpListenerContext context = listener.EndGetContext(result);
            HttpListenerRequest request = context.Request;
            string token = request.QueryString["oauth_token"];
            string verifier = request.QueryString["oauth_verifier"];

            // Obtain a response object.
            HttpListenerResponse response = context.Response;

            string responseString = "<HTML><BODY>You have been authenticated. Please return to the application. <script language=\"javascript\">window.close();</script></BODY></HTML>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);

            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();

            response.Close();

            listener.Close();

            TokenHolder.Instance.AccessTokenResponse = OAuthUtility.GetAccessToken(
                  ConsumerKey,
                  ConsumerSecret,
                  token,
                  verifier);
        }
    }
}
