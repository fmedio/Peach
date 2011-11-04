using System.IO;
using System.Text;
using NUnit.Framework;

namespace Peach.Test
{
    // Author: Fabrice Medio <fmedio@gmail.com>
    // Released under Creative Commons CC0 terms
    // http://creativecommons.org/publicdomain/zero/1.0/legalcode

    [TestFixture]
    public class TagTest
    {
        [Test]
        public void TestRender()
        {
            Assert.AreEqual("<foo />", Render(new Tag("foo")));
            Assert.AreEqual("<foo>bar</foo>", Render(new Tag("foo").Add("bar")));
            Assert.AreEqual("<foo one=\"one\" one=\"two\" hello=\"world\">bar<panda /></foo>", 
                            Render(new Tag("foo")
                                       .Add("bar")
                                       .Attr("one", "one")
                                       .Attr("one", "two")
                                       .Attr("hello", "world")
                                       .Add(new Tag("panda"))
                                ));

        }

        public static string Render(IRenderable renderable)
        {
            var memoryStream = new MemoryStream();
            var streamWriter = new StreamWriter(memoryStream, Encoding.UTF8);            
            streamWriter.AutoFlush = true;
            renderable.Render(streamWriter);
            memoryStream.Seek(0, SeekOrigin.Begin);
            return new StreamReader(memoryStream, Encoding.UTF8).ReadToEnd();
        }
    }
}