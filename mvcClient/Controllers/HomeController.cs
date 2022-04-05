using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using mvcClient.Models;
using Newtonsoft.Json;

namespace mvcClient.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
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

    [HttpGet]
    public async Task<IActionResult> Weather()
    {
        var data = new List<WeatherData>();
        using (var client = new HttpClient())
        {
            var result = client.GetAsync("https://localhost:5445/weatherforecast").Result;
            if (result.IsSuccessStatusCode)
            {
                var model = result.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject<List<WeatherData>>(model);
                return View(data);
            }
            else
            {
                throw new Exception("Unauthorised");
            }
        };
    }
}
