using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CCNet.SharedVersionLabeller.Plugin;
using Moq;
using ThoughtWorks.CruiseControl.Core;

namespace CCNet.SharedVersionLabeller.Tests
{
    [TestClass]
    public class SharedVersionLabellerTest
    {
        [TestMethod]
        public void TestSimple()
        {
            var l = new Labeller();
            l.StateFileName = "TestSimple.xml";

            var intResult = Mock.Of<IIntegrationResult>(x => x.Succeeded == true);
            Assert.AreEqual("1", l.Generate(intResult));
            Assert.AreEqual("2", l.Generate(intResult));
            Assert.AreEqual("3", l.Generate(intResult));
        }

        [TestMethod]
        public void TestPrefix()
        {
            var l = new Labeller();
            l.StateFileName = "TestPrefix.xml";
            l.Prefix = "Foo";

            var intResult = Mock.Of<IIntegrationResult>(x => x.Succeeded == true);
            Assert.AreEqual("Foo1", l.Generate(intResult));
            Assert.AreEqual("Foo2", l.Generate(intResult));
            Assert.AreEqual("Foo3", l.Generate(intResult));
        }

        [TestMethod]
        public void TestMultiplePrefixes()
        {
            var fooLabel = new Labeller();
            fooLabel.StateFileName = "TestMultiplePrefixes.xml";
            fooLabel.Prefix = "Foo";

            var barLabel = new Labeller();
            barLabel.StateFileName = "TestMultiplePrefixes.xml";
            barLabel.Prefix = "Bar";

            var intResult = Mock.Of<IIntegrationResult>(x => x.Succeeded == true);
            Assert.AreEqual("Foo1", fooLabel.Generate(intResult));
            Assert.AreEqual("Bar1", barLabel.Generate(intResult));
            Assert.AreEqual("Bar2", barLabel.Generate(intResult));
            Assert.AreEqual("Foo2", fooLabel.Generate(intResult));
            Assert.AreEqual("Foo3", fooLabel.Generate(intResult));
        }

        //[TestMethod]
        //public void TestNoSuccess()
        //{
        //    var l = new Labeller();
        //    l.StateFileName = "TestNoSuccess.xml";

        //    var successResult = Mock.Of<IIntegrationResult>(x => x.Succeeded == true);
        //    var noSuccessResult = Mock.Of<IIntegrationResult>();

        //    Assert.AreEqual("1", l.Generate(successResult));
        //    Assert.AreEqual("1", l.Generate(noSuccessResult));
        //    Assert.AreEqual("2", l.Generate(successResult));
        //}


    }
}
