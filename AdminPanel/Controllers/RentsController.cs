﻿using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    public class RentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
