using dotNetCoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetCoreMVC.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepo _pieRepo;
        private readonly ICategoryRepo _categoryRepo;

        public PieController(IPieRepo pieRepo, ICategoryRepo categoryRepo)
        {
            _pieRepo = pieRepo;
            _categoryRepo = categoryRepo;
        }

        /// <summary>
        /// List a view with the list of all pies
        /// </summary>
        /// <returns></returns>
        public ViewResult List()
        {
            return View(_pieRepo.GetAllPies);
        }
    }
}
