
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Adventure.Controller;

namespace Adventure.Test
{
    /// <summary>
    /// Tests that basic inputs work correctly.
    /// </summary>
    [TestClass]
    public class BasicInputTests
    {
        /// <summary>
        /// Tests that empty input is not acceptable; or that attempting to create a sentence
        /// from an empty string fails and returns an error message.
        /// </summary>
        [TestMethod]
        public void EmptyInput()
        {
            Sentence s = new Sentence("", out string e);
            Assert.IsTrue(e != null);
        }

        /// <summary>
        /// Tests that an input of whitespace is not acceptable; or that attempting to create
        /// a sentence from a string of only whitespace fails and returns an error message.
        /// </summary>
        [TestMethod]
        public void WhitespaceInput()
        {
            Sentence s1 = new Sentence(" ", out string e1);
            Assert.IsTrue(e1 != null);
            Sentence s2 = new Sentence("\t", out string e2);
            Assert.IsTrue(e2 != null);
            Sentence s3 = new Sentence(" \t  ", out string e3);
            Assert.IsTrue(e3 != null);
        }

        /// <summary>
        /// Tests that an input of only removable characters is not acceptable; or that attempting to
        /// create a sentence from a string of only removable characters fails and returns an error message.
        /// </summary>
        [TestMethod]
        public void InvalidCharInput()
        {
            Sentence s1 = new Sentence("}[/", out string e1);
            Assert.IsTrue(e1 != null);
        }

        /// <summary>
        /// Tests that an input of only removable words is not acceptable; or that attempting to
        /// create a sentence from a string of only removable words fails and returns an error message.
        /// </summary>
        [TestMethod]
        public void InvalidWordInput()
        {
            Sentence s1 = new Sentence("the", out string e1);
            Assert.IsTrue(e1 != null);
            Sentence s2 = new Sentence("a an the", out string e2);
            Assert.IsTrue(e2 != null);
        }
    }
}
