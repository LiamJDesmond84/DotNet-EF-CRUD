using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DotNet_EF_CRUD.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace DotNet_EF_CRUD.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private MyContext _dbContext;

    public HomeController(MyContext context, ILogger<HomeController> logger)
    {
        _logger = logger;
        _dbContext = context;
    }

    public IActionResult Index()
    {

        List<Dish> dishes = _dbContext.Dishes.ToList();
        return View(dishes);
    }

    public IActionResult Form()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult SubmitRecipe(Dish dish)
    {

        _dbContext.Add(dish);
        _dbContext.SaveChanges();
        Debug.WriteLine(dish.Name);
        Debug.WriteLine(dish.Chef);
        Debug.WriteLine(dish.Tastiness);
        Debug.WriteLine(dish.Calories);
        Debug.WriteLine(dish.Description);

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult ViewDish(int dishId)
    {
        Dish? dish = _dbContext.Dishes.FirstOrDefault(x => x.Id == dishId);

    }

}
