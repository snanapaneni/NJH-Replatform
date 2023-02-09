using CMS.Core;
using CMS.Scheduler;
using Njh.Kernel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Njh.Kernel.ScheduledTasks
{
    public class PressGaneyUpdateTask : ITask
    {
        /// <summary>
        /// Executes the task.
        /// </summary>
        /// <param name="ti">
        /// Info object representing the scheduled task.
        /// </param>
        /// <returns>
        /// null, if the task executed successfully.
        /// An error message, otherwise.
        /// </returns>
        public string Execute(TaskInfo ti)
        {
            string details = "Press Ganey Update Task executed. Task data: " + ti.TaskData;

            // Logs the execution of the task in the event log
            IEventLogService eventLogService = Service.Resolve<IEventLogService>();
            IPressGaneyService pressGaneyService = Service.Resolve<IPressGaneyService>();
            

            eventLogService.LogInformation("PressGaneyUpdateTask", "ExecuteStart", details);
            var message = pressGaneyService.RefreshCache(false);
            eventLogService.LogInformation("PressGaneyUpdateTask", "ExecuteEnd", message);

            // Returns a null value to indicate that the task executed successfully
            // Return an error message string with details in cases where the execution fails
            return null;
        }
    }
}