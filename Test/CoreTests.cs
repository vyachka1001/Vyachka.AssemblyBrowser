using NUnit.Framework;
using System.Collections.Generic;
using System.Reflection;
using Core.Browser;

namespace CoreTest
{
    [TestFixture]
    public class Tests
    {
        private const BindingFlags FLAGS = BindingFlags.NonPublic | BindingFlags.Public |
                                                        BindingFlags.Static | BindingFlags.Instance;
        public AssemblyBrowser browser = new("./Core.dll");

        [Test]
        public void TestFieldSignature()
        {
            var field = typeof(TestClass).GetFields(FLAGS)[0];
            Assert.AreEqual("private Double testField",
                browser.GetSignature(field));
        }

        [Test]
        public void TestMethodSignature()
        {
            var method = typeof(TestClass).GetMethods(FLAGS)[2];
            Assert.AreEqual("public Void TestMethod(Int32, Double)",
                browser.GetSignature(method));
        }

        [Test]
        public void TestPropertySignature()
        {
            var propertie = typeof(TestClass).GetProperties()[0];
            Assert.AreEqual("public Double TestField get; private set; ",
                browser.GetSignature(propertie));
        }

        [Test]
        public void TestUnsupportedMember()
        {
            var constructor = typeof(TestClass).GetConstructors()[0];
            Assert.AreEqual("<unsupported member>",
                browser.GetSignature(constructor));
        }
    }

    public class TestClass
    {
        private double testField;

        public double TestField
        {
            get => testField;
            private set => testField = value;
        }

        public TestClass()
        {
        }

        public void TestMethod(int a, double b)
        {
        }
    }

}