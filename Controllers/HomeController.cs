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

    // FIX THIS - https://www.tutorialsteacher.com/mvc/routing-in-mvc
    [HttpGet("/Home/ViewDish/{dishId}")]
    public IActionResult ViewDish(int dishId)
    {
        Dish? dish = _dbContext.Dishes.FirstOrDefault(x => x.Id == dishId);

        return View(dish);
    }

    [HttpGet("/Home/EditDish/{dishId}")]
    public IActionResult EditDish(int dishId)
    {
        Dish? dish = _dbContext.Dishes.FirstOrDefault(x => x.Id == dishId);
        return View(dish);
    }


    [HttpPost("/Home/UpdateDish/{dishId}")]
    public IActionResult UpdateDish(Dish dish, int dishId)
    {
        Dish? currentDish = _dbContext.Dishes.FirstOrDefault(x => x.Id == dishId);


        currentDish.Name = dish.Name;
        currentDish.Chef = dish.Chef;
        currentDish.Calories = dish.Calories;
        currentDish.Tastiness = dish.Tastiness;
        currentDish.Description = dish.Description;
        dish.UpdatedAt = DateTime.Now;

        _dbContext.SaveChanges();
        return RedirectToAction("Index");

    }

    //[HttpGet("/Home/DeleteDish/{dishId}")]
    public IActionResult DeleteDish(int dishId)
    {
        // Like Update, we will need to query for a single Model/Object from our Context object
        Dish? RetrievedObject = _dbContext.Dishes.SingleOrDefault(x => x.Id == dishId);

        // Then pass the object we queried for to .Remove() on Users
        _dbContext.Dishes.Remove(RetrievedObject);

        // Finally, .SaveChanges() will remove the corresponding row representing this User from DB 
        _dbContext.SaveChanges();

        return RedirectToAction("Index");


    }

}
