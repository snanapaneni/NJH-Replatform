using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Njh.Kernel.Models.Dto;

namespace Njh.Kernel.Services
{
    public interface IAccordionService
    {
        public IEnumerable<AccordionItem> GetItems(string path);
    }
}
