using dotNetCoreMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetCoreMVC.Components
{
    public class CategoryDropDown : ViewComponent
    {
        private readonly ICategoryRepo _categoryRepo;
        public CategoryDropDown(ICategoryRepo categoryRepo) => _categoryRepo = categoryRepo;
        public IViewComponentResult Invoke()
        {
            var allCategories = _categoryRepo.GetAllCategories;
            return View(allCategories);
        }
    }
}
