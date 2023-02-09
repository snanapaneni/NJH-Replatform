using Njh.Kernel.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Njh.Kernel.Services
{
    public interface IPressGaneyService
    {
        string RefreshCache(bool initialRefresh);
        PressGaneyInfo PopulateData(string id);
        PressGaneyInfo FetchAllPressGaneyData(string id, PressGaneyInfo pressGaneyInfo);
        PressGaneyInfo PopulateCommentData(string id);
        PressGaneyInfo PopulateRatingData(string id);
    }
}
