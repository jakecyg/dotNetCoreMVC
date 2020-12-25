using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetCoreMVC.Models
{
    public interface ICategoryRepo
    {
        IEnumerable<Category> GetAllCategories { get; }
    }
}
