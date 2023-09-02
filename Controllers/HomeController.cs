using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DotNet_EF_CRUD.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Threading;

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

    // Create
    public IActionResult SubmitRecipe(Dish dish)
    {
        _dbContext.Add(dish);
        _dbContext.SaveChanges();

        return RedirectToAction("Index");
    }

    // Read
    // https://www.tutorialsteacher.com/mvc/routing-in-mvc
    [HttpGet]
    public IActionResult ViewDish(int id)
    {
        Dish? dish = _dbContext.Dishes.FirstOrDefault(x => x.Id == id);

        return View(dish);
    }

    public IActionResult EditDish(int id)
    {
        Dish? dish = _dbContext.Dishes.FirstOrDefault(x => x.Id == id);
        return View(dish);
    }

    // Update
    [HttpPost]
    public IActionResult UpdateDish(Dish dish)
    {
        Dish? currentDish = _dbContext.Dishes.FirstOrDefault(x => x.Id == dish.Id);


        currentDish.Name = dish.Name;
        currentDish.Chef = dish.Chef;
        currentDish.Calories = dish.Calories;
        currentDish.Tastiness = dish.Tastiness;
        currentDish.Description = dish.Description;
        dish.UpdatedAt = DateTime.Now;

        _dbContext.SaveChanges();
        return RedirectToAction("Index");

    }

    // Delete
    [HttpGet]
    public IActionResult DeleteDish(int id)
    {
        // Like Update, we will need to query for a single Model/Object from our Context object
        Dish? RetrievedObject = _dbContext.Dishes.SingleOrDefault(x => x.Id == id);

        // Then pass the object we queried for to .Remove() on Users
        _dbContext.Dishes.Remove(RetrievedObject);

        // Finally, .SaveChanges() will remove the corresponding row representing this User from DB 
        _dbContext.SaveChanges();

        return RedirectToAction("Index");


    }

}
