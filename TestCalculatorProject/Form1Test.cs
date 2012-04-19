using SCalculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestCalculatorProject
{
    
    
    /// <summary>
    ///This is a test class for Form1Test and is intended
    ///to contain all Form1Test Unit Tests
    ///</summary>
    [TestClass()]
    public class Form1Test
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Add
        ///</summary>
        [TestMethod()]
        public void AddTest()
        {
            Form1 target = new Form1(); // TODO: Initialize to an appropriate value
            string numeros = string.Empty; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.Add(numeros);
            Assert.AreEqual(expected, actual);

            numeros = "1,2"; // TODO: Initialize to an appropriate value
            expected = 3; // TODO: Initialize to an appropriate value            
            actual = target.Add(numeros);
            Assert.AreEqual(expected, actual);

            numeros =  "1\n2,3"; // TODO: Initialize to an appropriate value
            expected = 6; // TODO: Initialize to an appropriate value            
            actual = target.Add(numeros);
            Assert.AreEqual(expected, actual);

            numeros =  "1,\n"; // TODO: Initialize to an appropriate value
            expected = 1; // TODO: Initialize to an appropriate value            
            actual = target.Add(numeros);
            Assert.AreEqual(expected, actual);

            numeros = "1001,2"; // TODO: Initialize to an appropriate value
            expected = 2; // TODO: Initialize to an appropriate value            
            actual = target.Add(numeros);
            Assert.AreEqual(expected, actual);

            numeros = "//;\n1;2;3"; // TODO: Initialize to an appropriate value
            expected = 6; // TODO: Initialize to an appropriate value            
            actual = target.Add(numeros);
            Assert.AreEqual(expected, actual);

            numeros = "//;\n12;2;3"; // TODO: Initialize to an appropriate value
            expected = 17; // TODO: Initialize to an appropriate value            
            actual = target.Add(numeros);
            Assert.AreEqual(expected, actual);

            numeros = "//[***]\n1***2***8"; // TODO: Initialize to an appropriate value
            expected = 11; // TODO: Initialize to an appropriate value            
            actual = target.Add(numeros);
            Assert.AreEqual(expected, actual);

            numeros = "//;\n12,5,3;6"; // TODO: Initialize to an appropriate value
            expected = 26; // TODO: Initialize to an appropriate value            
            actual = target.Add(numeros);
            Assert.AreEqual(expected, actual);

            numeros = "//[**][%%]\n1**2%%3"; // TODO: Initialize to an appropriate value
            expected = 6; // TODO: Initialize to an appropriate value            
            actual = target.Add(numeros);
            Assert.AreEqual(expected, actual);
        }        
    }
}
