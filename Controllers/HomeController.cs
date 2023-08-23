﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DotNet_EF_CRUD.Models;
using Microsoft.EntityFrameworkCore;

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
        return View();
    }

    [HttpGet]
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
        Debug.WriteLine(dish.Name);

        return View();
    }

}
