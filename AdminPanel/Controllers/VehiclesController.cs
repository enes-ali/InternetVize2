﻿using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class VehiclesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
