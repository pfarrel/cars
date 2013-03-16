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
                var make = context.Makes.FirstOrDefault();
                Assert.IsNull(make);
            }
        }
    }
}
