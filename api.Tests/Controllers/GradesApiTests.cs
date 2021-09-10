using api.Controllers;
using api.Tests.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace api.Tests.Controllers
{
    [TestClass]
    public class GradesApiTests
    {
        [TestMethod]
        public void Get()
        {
            GradesController controller = new GradesController();
            IEnumerable<GradesDbModel.ApiModel> items = controller.Get(1, 10);
            Assert.AreEqual(items.Count(), 10);
         }
    }
}
