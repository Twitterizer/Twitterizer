using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using Twitterizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Twitterizer2.MSTestCases;

namespace Twitterizer2.TestMethodCases
{
    [TestClass]
    public class CoreTestMethods
    {
        [TestMethod]
        [Ignore()] // This test seems totally broken
        public void Serialization()
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

            var interfacesToImplement = new List<Type>();
            interfacesToImplement.Add(twitterizerAssembly.GetType("Twitterizer.Core.ICommand`1"));
            //interfacesToImplement.Add(twitterizerAssembly.GetType("Twitterizer.Core.ITwitterCommand`1"));
            Assert.AreEqual(0, interfacesToImplement.Where(x => x == null).Count(), "interfacesToImplement contains null values");

            var baseClassesToInherit = new List<Type>();
            baseClassesToInherit.Add(twitterizerAssembly.GetType("Twitterizer.Commands.PagedTimelineCommand`1"));
            Assert.AreEqual(0, baseClassesToInherit.Where(x => x == null).Count(), "baseClassesToInherit contains null values");

            var commandTypesToCheck =
                (from t in twitterizerAssembly.GetTypes()
                 where
                     (
                         interfacesToImplement.Intersect(t.GetInterfaces()).Count() > 0 ||
                         baseClassesToInherit.Any(t.IsSubclassOf)
                     )
                 select t)
                    .ToList();
            Assert.AreEqual(0, commandTypesToCheck.Where(x => x == null).Count(), "commandTypesToCheck contains null values");

            var objects = objectTypesToCheck.Union(commandTypesToCheck).ToList();
            Assert.AreEqual(0, objects.Where(x => x == null).Count(), "objects contains null values");

            foreach (Type type in objects)
            {
                Console.WriteLine(string.Format("Inspecting: {0}", type.FullName));

                // Check that the object itself is marked as serializable
                Assert.IsTrue(type.IsSerializable, string.Format("{0} is not marked as Serializable", type.Name));

                // Get the parameter-less constructor, if there is one
                ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);

                if (constructor == null)
                    Assert.Fail(string.Format("{0} does not have a public ctor", type.FullName));

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

        [TestMethod]
        public void SSL()
        {
            TwitterResponse<TwitterUser> sslUser = TwitterUser.Show("twitterapi", new OptionalProperties { UseSSL = true });
            Assert.IsTrue(sslUser.RequestUrl.StartsWith("https://"), "sslUser connection should be SSL");

            TwitterResponse<TwitterUser> user = TwitterUser.Show("twitterapi", new OptionalProperties { UseSSL = false });
            Assert.IsTrue(user.RequestUrl.StartsWith("http://"), "user connection should not be SSL");

            TwitterResponse<TwitterStatusCollection> timeline = TwitterTimeline.HomeTimeline(Configuration.GetTokens(), new TimelineOptions { UseSSL = true });
            Assert.IsTrue(timeline.RequestUrl.StartsWith("https://"), "timeline connection should be SSL");
        }

        [TestMethod]
        public void CheckAssemblySignature()
        {
            Assembly asm = Assembly.GetAssembly(typeof(TwitterUser));
            if (asm != null)
            {
                AssemblyName asmName = asm.GetName();
                byte[] key = asmName.GetPublicKey();
                Assert.IsTrue(key.Length > 0);
            }
        }
    }
}
