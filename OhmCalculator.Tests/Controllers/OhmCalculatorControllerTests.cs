using Microsoft.VisualStudio.TestTools.UnitTesting;
using OhmCalculator.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OhmCalculator.Controllers.Tests
{
    [TestClass()]
    public class OhmCalculatorControllerTests
    {        
        [TestMethod()]
        public void IndexTest()
        {
            OhmCalculatorController cntrlr = new OhmCalculatorController();
            var indexView = cntrlr.Index();

            
        }

        [TestMethod()]
        public void CalculateTest()
        {
            Assert.Fail();
        }
    }
}