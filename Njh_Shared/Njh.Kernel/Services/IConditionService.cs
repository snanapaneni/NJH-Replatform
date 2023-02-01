using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Njh.Kernel.Kentico.Models.PageTypes;

namespace Njh.Kernel.Services
{
    public interface IConditionService
    {
        IEnumerable<PageType_Condition> GetConditions(string path, int nestedLevel, string[] orderBy);
    }
}
