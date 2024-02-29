using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Test_Task_.Net_Reenbit_Masksym_Sheremeta.Models;
using Test_Task_.Net_Reenbit_Masksym_Sheremeta.Services;

namespace Test_Task_.Net_Reenbit_Masksym_Sheremeta.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private static IConfiguration? _configuration;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(Blob blob)
    {
        FileService fileService = new(_configuration);
        
        if (ModelState.IsValid)
        {
            var url =  fileService.UploadFileAsync(blob);
        }
        
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}