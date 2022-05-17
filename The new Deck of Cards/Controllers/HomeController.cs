using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using The_new_Deck_of_Cards.Models;

namespace The_new_Deck_of_Cards.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://deckofcardsapi.com/api/deck/new/shuffle/?deck_count=1");

            HttpResponseMessage response = await client.SendAsync(request);
            string stringResponse = await response.Content.ReadAsStringAsync();

            var r = JsonConvert.DeserializeObject<Shuffleupagus>(stringResponse);
            
            
            HttpRequestMessage deckRequest = new HttpRequestMessage(HttpMethod.Get, $"https://deckofcardsapi.com/api/deck/{r.deck_id}/draw/?count=5");

            HttpResponseMessage answer = await client.SendAsync(deckRequest);
            string stringAnswer = await answer.Content.ReadAsStringAsync();

            var c = JsonConvert.DeserializeObject<Deck>(stringAnswer);
            return View(c);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}