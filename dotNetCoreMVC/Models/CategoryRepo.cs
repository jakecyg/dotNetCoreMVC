using dotNetCoreMVC.Common.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetCoreMVC.Models
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly dbContext _db;
        public CategoryRepo(dbContext db) => _db = db;
        public IEnumerable<Category> GetAllCategories => _db.Categories;
    }
}
