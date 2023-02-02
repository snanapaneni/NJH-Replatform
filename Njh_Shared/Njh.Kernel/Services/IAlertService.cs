using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Njh.Kernel.Kentico.Models.PageTypes;

namespace Njh.Kernel.Services
{
    public interface IAlertService
    {
        PageType_Alert FirstOrDefault(string path);
    }
}
