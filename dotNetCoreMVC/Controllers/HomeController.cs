using dotNetCoreMVC.Models;
using dotNetCoreMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetCoreMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepo _pieRepo;

        public HomeController(IPieRepo pieRepo) => _pieRepo = pieRepo;
        public IActionResult Index()
        {
            var vm = new HomeVM();
            vm.PiesOfTheWeek = _pieRepo.GetAllPiesOfTheWeek;
            return View(vm);
        }
    }
}
