using System.Linq;
using NUnit.Framework;

namespace Peach.Test
{
    // Author: Fabrice Medio <fmedio@gmail.com>
    // Released under Creative Commons CC0 terms
    // http://creativecommons.org/publicdomain/zero/1.0/legalcode

    [TestFixture]
    public class BagTest
    {
        [Test]
        public void TestPut()
        {
            var bag = new Bag<string, string>();
            bag["foo"] = "hello";
            bag["foo"] = "world";

            Assert.AreEqual("hello", bag["foo"]);
            Assert.AreEqual(2, bag.GetValues("foo").Count());
        }
    }
}