using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AzureFunctionTangyWeb.Models;
using Newtonsoft.Json;

namespace AzureFunctionTangyWeb.Controllers;

public class HomeController(IHttpClientFactory httpClientFactory) : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    // http://localhost:7120/api/OnSalesUploadWriteToQueue
    
    [HttpPost]
    public async Task<IActionResult> Index(SalesRequest salesRequest)
    {
        salesRequest.Id = Guid.NewGuid().ToString();
        using var client = httpClientFactory.CreateClient();

        using (var content = new StringContent(JsonConvert.SerializeObject(salesRequest), System.Text.Encoding.UTF8,
                   "application/json"))
        {
           HttpResponseMessage response = await client.PostAsync("http://localhost:7120/api/OnSalesUploadWriteToQueue", content);
           string returnValue = await response.Content.ReadAsStringAsync();
        }
        
        return RedirectToAction(nameof(Index));
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