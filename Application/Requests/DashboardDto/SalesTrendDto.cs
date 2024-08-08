using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Requests.DashboardDto
{
    public record SalesTrendDto
    {
        public string Year { get; set; }
        public string Month { get; set; }
        public double Sales { get; set; }
    }
}
