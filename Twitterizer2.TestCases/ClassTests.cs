namespace Twitterizer2.TestCases
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Reflection;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Xml.Serialization;
    using NUnit.Framework;
    using Twitterizer;
    using System.IO;
    using System.Runtime.Serialization;

    [TestFixture]
    public class ClassTests
    {
        [Test]
        public static void Serialization()
        {
            Assembly twitterizerAssembly = Assembly.GetAssembly(typeof(TwitterUser));
            foreach (Type type in twitterizerAssembly.GetExportedTypes())
            {
                if (type.IsAbstract || type.IsInterface)
                    continue;

                if (
                    type.GetInterfaces().Contains(twitterizerAssembly.GetType("Twitterizer.Core.ITwitterObject")) ||
                    type.IsSubclassOf(twitterizerAssembly.GetType("Twitterizer.Entities.TwitterEntity"))
                    )
                {
                    Console.WriteLine(string.Format("Inspecting: {0}", type.FullName));

                    // Check that the object itself is marked as serializable
                    Assert.That(type.IsSerializable, string.Format("{0} is not marked as Serializable", type.Name));

                    // Now, actually attempt to serialize it.
                    ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);
                    if (constructor != null)
                    {
                        object objectToSerialize = constructor.Invoke(null);
                        try
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                new BinaryFormatter().Serialize(ms, objectToSerialize);
                            }
                        }
                        catch (Exception)
                        {
                            Assert.Fail(string.Format("{0} could not be serialized", type.FullName));
                        }
                    }
                }
            }
        }
    }
}
