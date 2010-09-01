namespace Twitterizer2.TestCases
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization.Formatters.Binary;
    using NUnit.Framework;
    using Twitterizer;

    [TestFixture]
    public class CoreTests
    {
        [Test]
        [Category("Read-Only")]
        public static void Serialization()
        {
            Assembly twitterizerAssembly = Assembly.GetAssembly(typeof(TwitterUser));
            var objectTypesToCheck = from t in twitterizerAssembly.GetExportedTypes()
                               where !t.IsAbstract &&
                               !t.IsInterface &&
                               (
                                t.GetInterfaces().Contains(twitterizerAssembly.GetType("Twitterizer.Core.ITwitterObject")) ||
                                t.IsSubclassOf(twitterizerAssembly.GetType("Twitterizer.Entities.TwitterEntity"))
                               )
                               select t;

            var commandTypesToCheck = from t in twitterizerAssembly.GetTypes()
                                      where
                                     //!t.IsAbstract &&
                                     //!t.IsInterface &&
                                     (
                                      t.GetInterfaces().Contains(twitterizerAssembly.GetType("Twitterizer.Core.ICommand`1")) ||
                                      t.GetInterfaces().Contains(twitterizerAssembly.GetType("Twitterizer.Core.TwitterCommand`1")) ||
                                      t.IsSubclassOf(twitterizerAssembly.GetType("Twitterizer.Commands.PagedTimelineCommand`1")) ||
                                      t.IsSubclassOf(twitterizerAssembly.GetType("Twitterizer.Core.PagedCommand`1")) ||
                                      t.IsSubclassOf(twitterizerAssembly.GetType("Twitterizer.Core.CursorPagedCommand`1"))
                                     )
                                     select t;

            foreach (Type type in objectTypesToCheck.Union(commandTypesToCheck))
            {
                Console.WriteLine(string.Format("Inspecting: {0}", type.FullName));

                // Check that the object itself is marked as serializable
                Assert.That(type.IsSerializable, string.Format("{0} is not marked as Serializable", type.Name));

                // Get the parameter-less constructor, if there is one
                ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);

                // Instantiate the type by invoking the constructor
                object objectToSerialize = constructor.Invoke(null);

                try
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        // Serialize the object
                        new BinaryFormatter().Serialize(ms, objectToSerialize);
                    }
                }
                catch (Exception) // Catch any exceptions and assert a failure
                {
                    Assert.Fail(string.Format("{0} could not be serialized", type.FullName));
                }
            }
        }

        [Test]
        [Category("Read-Only")]
        public static void SSL()
        {
            TwitterResponse<TwitterUser> sslUser = TwitterUser.Show("twitterapi", new OptionalProperties() { UseSSL = true });
            Assert.That(sslUser.RequestUrl.StartsWith("https://"));
            
            TwitterResponse<TwitterUser> user = TwitterUser.Show("twitterapi", new OptionalProperties() { UseSSL = false });
            Assert.That(user.RequestUrl.StartsWith("http://"));

            TwitterResponse<TwitterStatusCollection> timeline = TwitterTimeline.HomeTimeline(Configuration.GetTokens(), new TimelineOptions() { UseSSL = true });
            Assert.That(timeline.RequestUrl.StartsWith("https://"));
            Assert.That(user.RequestUrl.StartsWith("https://"));
        }
    }
}
