﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebProjVet.Controllers
{
    public class DTSampleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}