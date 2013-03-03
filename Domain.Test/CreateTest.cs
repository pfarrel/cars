using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain.Test
{
    [TestClass]
    public class CreateTest
    {
        [TestMethod]
        public void CreateWorks()
        {
            using (var context = new CarsContext())
            {
                //Assert.AreEqual(0, context.Makes.ToList().Count());
            }
        }
    }
}
