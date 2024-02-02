using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.DTOs.CategoryDTOs
{
    public class UpdateCategoryDto
    {
        public string Name { get; set; }
        public string UrlHandle { get; set; }
        public string Description { get; set; }
    }
}
