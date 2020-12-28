using dotNetCoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetCoreMVC.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Pie> PiesOfTheWeek { get; set; }
    }
}
