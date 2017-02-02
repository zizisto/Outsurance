using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Outsurance;

namespace OutsuranceTest
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()
        {
          SortingClass.PrintNameAndSurname(SortingClass.GetCsvData());
        }

        [TestMethod]
        public void TestMethod2()
        {
            SortingClass.PrintAddress(SortingClass.GetCsvData());
        }
    }
}
