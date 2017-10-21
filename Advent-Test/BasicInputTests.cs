
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Adventure.Controller;
using System;

namespace Adventure.Test
{
    /// <summary>
    /// Tests that basic inputs work correctly.
    /// </summary>
    [TestClass]
    public class BasicInputTests
    {
        /// <summary>
        /// Tests that attempting to create a sentence from a null string throws an exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullSentenceInput()
        {
            Sentence s = new Sentence(null, out string e);
        }

        /// <summary>
        /// Tests that empty player input is not acceptable; or that attempting to create a sentence
        /// from an empty string fails and returns an error message.
        /// </summary>
        [TestMethod]
        public void EmptySentenceInput()
        {
            Sentence s = new Sentence("", out string e);
            Assert.IsTrue(e != null);
        }

        /// <summary>
        /// Tests that a player input of whitespace is not acceptable; or that attempting to create
        /// a sentence from a string of only whitespace fails and returns an error message.
        /// </summary>
        [TestMethod]
        public void WhitespaceSentenceInput()
        {
            Sentence s1 = new Sentence(" ", out string e1);
            Assert.IsTrue(e1 != null);
            Sentence s2 = new Sentence("\t", out string e2);
            Assert.IsTrue(e2 != null);
            Sentence s3 = new Sentence(" \t  ", out string e3);
            Assert.IsTrue(e3 != null);
        }

        /// <summary>
        /// Tests that a player input of only removable characters is not acceptable; or that attempting to
        /// create a sentence from a string of only removable characters fails and returns an error message.
        /// </summary>
        [TestMethod]
        public void InvalidCharSentenceInput()
        {
            Sentence s1 = new Sentence("}[/", out string e1);
            Assert.IsTrue(e1 != null);
        }

        /// <summary>
        /// Tests that a player input of only removable words is not acceptable; or that attempting to
        /// create a sentence from a string of only removable words fails and returns an error message.
        /// </summary>
        [TestMethod]
        public void InvalidWordSentenceInput()
        {
            Sentence s1 = new Sentence("the", out string e1);
            Assert.IsTrue(e1 != null);
            Sentence s2 = new Sentence("a an the", out string e2);
            Assert.IsTrue(e2 != null);
        }
    }
}
