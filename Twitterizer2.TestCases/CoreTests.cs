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
            foreach (Type type in twitterizerAssembly.GetExportedTypes())
            {
                // Skip abstract classes and interfaces
                if (type.IsAbstract || type.IsInterface)
                    continue;

                // Check if the type inherits from my common interface or inherits another common base class
                if (
                    !type.GetInterfaces().Contains(twitterizerAssembly.GetType("Twitterizer.Core.ITwitterObject")) &&
                    !type.IsSubclassOf(twitterizerAssembly.GetType("Twitterizer.Entities.TwitterEntity"))
                    )
                    continue;

                Console.WriteLine(string.Format("Inspecting: {0}", type.FullName));

                // Check that the object itself is marked as serializable
                Assert.That(type.IsSerializable, string.Format("{0} is not marked as Serializable", type.Name));

                // Get the parameter-less constructor, if there is one
                ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);

                // If there isn't a parameter-less constructor, skip the type
                if (constructor == null)
                    continue;

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
            OptionalProperties options = new OptionalProperties()
            {
                UseSSL = true
            };

            TwitterUser user = TwitterUser.Show("twitterapi", options);

            Assert.That(user.RequestStatus.FullPath.StartsWith("https://"));
        }
    }
}
