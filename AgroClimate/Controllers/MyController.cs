using Microsoft.AspNetCore.Mvc;

namespace AgroClimate.Controllers;

public class MyController
{
    private readonly IConfiguration _configuration;

    public MyController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IActionResult Index()
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        // Use a string de conexão conforme necessário
        return View();
    }
}
