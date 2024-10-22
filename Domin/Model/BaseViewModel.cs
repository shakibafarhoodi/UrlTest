using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domin.ViewModel;

namespace Domin.Model
{
    public class BaseViewModel
    {
        public CreateUrlViewModel CreateUrl { get; set; }
        public List<string> Urls { get; set; }

    }


}
