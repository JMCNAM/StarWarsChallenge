using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using StartWarsChallenge.Models;
using StartWarsChallenge.Process;

namespace StartWarsUnitTest
{
    [TestClass]
    public class ProgramTests
    {
        [TestMethod]
        public void Main_Returns_List_Of_Starships()
        {
            var p = new ProcessClass();
            var res = p.ComputeStarshipResupply(0);
            Assert.AreEqual(res.GetType(), new Dictionary<Starship, string>().GetType());
        }
        [TestMethod]
        public void Main_Assigns_Distance_To_Ships()
        {
            var p = new ProcessClass();

            var res = p.ComputeStarshipResupply(1000000);
            var val1 = res.Where(x => x.Key.name == "Y-wing").ToList();
            var val2 = res.Where(x => x.Key.name == "Millennium Falcon").ToList();
            var val3 = res.Where(x => x.Key.name == "Rebel transport").ToList();

            Assert.AreEqual(val1[0].Value, "74");
            Assert.AreEqual(val2[0].Value, "9");
            Assert.AreEqual(val3[0].Value, "11");
        }
    }
}
