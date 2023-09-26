using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.Dtos
{
    public class ProductFilterDto
    {
        public string? Brands { get; set; }
        public string? Types { get; set; }
        public string? Sort { get; set; }
        public string? Search {  get; set; }
        public int? PageSize { get; set; } = 6;
        public int? PageIndex { get; set; } = 1; 
    }
}
