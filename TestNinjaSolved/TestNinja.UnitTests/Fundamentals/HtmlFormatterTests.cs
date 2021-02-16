﻿using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    public class HtmlFormatterTests
    {
        [Test]
        public void FormatAsBold_WhenCalled_ShouldEncloseStringInBoldElement()
        {
            var formatter = new HtmlFormatter();

            var result = formatter.FormatAsBold("hello");

            // Specific assertion
            Assert.That(result, Is.EqualTo("<strong>hello</strong>").IgnoreCase);

            // General assertion
            //Assert.That(result, Does.StartWith("<strong>"));
            //Assert.That(result, Does.EndWith("</strong>"));
            //Assert.That(result, Does.Contain("abc"));
        }
    }
}
