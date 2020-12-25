using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNetCoreMVC.Models
{
    public interface IPieRepo
    {
        IEnumerable<Pie> GetAllPies { get; }
        IEnumerable<Pie> GetAllPiesOfTheWeek { get; }
        Pie GetPieById(int id);
    }
}
