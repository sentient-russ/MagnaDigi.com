﻿using Microsoft.AspNetCore.Mvc;
using magnadigi.Models;
using System.Diagnostics;
using magnadigi.Data;


namespace magnadigi.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly magnadigiContext _context;


        public HomeController(ILogger<HomeController> logger, magnadigiContext context)
        {
            _logger = logger;
            _context = context;
            
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
        public IActionResult Services()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult CaseStudyMomPop()
        {
            return View();
        }
        public IActionResult CaseStudyMomPopERDiagram()
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