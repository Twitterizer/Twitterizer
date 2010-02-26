namespace Twitterizer
{
    using System;
    using Twitterizer.Core;
    public class CommandValidationException : ApplicationException
    {
        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>The command.</value>
        public Type Sender { get; set; }

        /// <summary>
        /// Gets or sets the name of the method.
        /// </summary>
        /// <value>The name of the method.</value>
        public string MethodName { get; set; }

        public CommandValidationException(Type sender, string methodName)
            : base()
        {
            this.Sender = sender;
            this.MethodName = methodName;
        }
    }
}
