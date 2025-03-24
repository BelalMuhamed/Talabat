using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Specifications
{
  public  class SpecProductParams
    {
       
        public string? sort { get; set; }
        public int? brandid { get; set; }
        public int? categoryid { get; set; }
        private int _pagesize=5;
        private string? searchbyname;

        public string? SearchByName
        {
            get { return searchbyname?.ToLower(); }
            set { searchbyname = value; }
        }
        public int PageSize
        {
            get { return _pagesize; }
            set { _pagesize = (value == 0 || value > 10) ? 10 : value; }
        }
        public int PageIndex { get; set; } = 1;

    }
}
