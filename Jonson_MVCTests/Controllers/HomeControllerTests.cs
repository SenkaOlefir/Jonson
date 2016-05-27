using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jonson_MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jonson_MVC.Controllers.Tests
{
    [TestClass()]
    public class HomeControllerTests
    {
        [TestMethod()]
        public void IndexTest()
        {
            var controller = new GraphController();
            var indexResult = controller.Index();

            Assert.Fail();
        }
    }
}