    using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nancy.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;

namespace MontyHallAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MontyHallController : ControllerBase
    {
        private readonly ILogger<MontyHallController> _logger;

        public MontyHallController(ILogger<MontyHallController> logger)
        {
            _logger = logger;
            Console.WriteLine("entered controller");
        }

        [HttpGet()]
        public string GetMontyHallPick(int simulations, bool switchdoor )
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage();
            var response = httpRequest.CreateResponse(HttpStatusCode.OK);
            var json = String.Empty;
            try
            {
                
                var rng = new Random();
                MontyHallPick objmontyhall = new MontyHallPick();
                int wins = 0;
                int losses = 0;

                for (int i = 0; i < simulations; i++)
                {
                    bool result = MontyHallPick(rng.Next(3), Convert.ToInt32(switchdoor), rng.Next(3), rng.Next(1));

                    if (result)
                    {
                        wins++;
                    }
                    else
                    {
                        losses++;
                    }
                }
                objmontyhall.wins = wins;
                objmontyhall.losses = losses;
                objmontyhall.simulations = simulations;
                objmontyhall.changechoice = switchdoor;
                json = new JavaScriptSerializer().Serialize(objmontyhall);
                //response.Content = new StringContent(json, Encoding.UTF8, "application/json");
                return json;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            return json;
        }

        public static bool MontyHallPick(int pickedDoor, int changedoor, int carDoor, int goatDoorToRemove )
        {
            bool win = false;
            int leftGoat = 0;
            int rightGoat = 0;
            int keepGoat = 0;

            switch(pickedDoor)
            {
                case 0: leftGoat = 1; rightGoat = 1; break;
                case 1: leftGoat = 0; rightGoat = 2; break;
                case 2: leftGoat = 0; rightGoat = 1; break;
            }
            keepGoat = goatDoorToRemove == 0 ? rightGoat : leftGoat;

            if(changedoor == 0)
            {
                win = carDoor == pickedDoor;
            }
            else
            {
                win = carDoor != keepGoat;
            }

            return win;
        }

    }
}
