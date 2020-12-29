using dotNetCoreMVC.Models;
using dotNetCoreMVC.ViewModels;
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
        public ViewResult PieList(string category)
        {
            IEnumerable<Pie> pies;
            string currentCategory;

            if (string.IsNullOrEmpty(category))
            {
                pies = _pieRepo.GetAllPies.OrderBy(x => x.Id);
                currentCategory = "All pies";
            }
            else
            {
                pies            = _pieRepo.GetAllPies
                                          .Where(x => x.Category.CategoryName == category)
                                         .OrderBy(p => p.Id);
                currentCategory = _categoryRepo.GetAllCategories.FirstOrDefault(x => x.CategoryName == category)?.CategoryName;
            }

            var vm = new PiesListVM
            {
                Pies = pies,
                CurrentCategory = currentCategory
            };
            return View(vm);
        }

        public IActionResult Details(int id)
        {
            var pie = _pieRepo.GetPieById(id);
            if (pie == null) return NotFound();
            return View(pie);
        }
    }
}
