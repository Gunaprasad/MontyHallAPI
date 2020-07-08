using Microsoft.VisualStudio.TestTools.UnitTesting;
using MontyHallAPI.Controllers;
using Microsoft.Extensions.Logging;
using Nancy.Json;
using System;
using Newtonsoft.Json;
using MontyHallAPI;

namespace MontyMStest
{
    [TestClass]
    public class UnitTest1
    {
        private readonly ILogger<MontyHallController> _logger;
        [TestMethod]
        public void MontyHallControllerTestforSwitchDoor()
        {

            MontyHallController obj = new MontyHallController(_logger);
            var result = obj.GetMontyHallPick(1000, true);
            String actual = "{\"simulations\":1000,\"changechoice\":true,\"wins\":647,\"losses\":353}";
            MontyHallPick monty = JsonConvert.DeserializeObject<MontyHallPick>(actual);
            MontyHallPick montyresult = JsonConvert.DeserializeObject<MontyHallPick>(result);
            Assert.IsTrue(monty.wins < montyresult.wins);
        }

        [TestMethod]
        public void MontyHallControllerTestForNoSwitch()
        {

            MontyHallController obj = new MontyHallController(_logger);
            var result = obj.GetMontyHallPick(1000, false);
            String actual = "{\"simulations\":1000,\"changechoice\":false,\"wins\":400,\"losses\":600}";
            MontyHallPick montyactual = JsonConvert.DeserializeObject<MontyHallPick>(actual);
            MontyHallPick montyresult = JsonConvert.DeserializeObject<MontyHallPick>(result);
            Assert.IsTrue(montyactual.losses <= montyresult.losses);
        }
    }
}
