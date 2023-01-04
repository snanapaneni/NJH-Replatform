

namespace Njh.Kernel.Repositories
{
    using CMS.CustomTables;
    using CMS.DataEngine;
    using Njh.Kernel.Models;
    using ReasonOne.Extensions;
    public class CustomTableRepository
       : ICustomTableRepository
    {
        private readonly ContextConfig context;

        public CustomTableRepository(
            ContextConfig context)
        {
            this.context = context;
        }

        public string EnabledColumnName { get; set; } =
            "Enabled";

        public T GetCustomTableItemByGuid<T>(
            Guid guid,
            IEnumerable<string> columnNames = null,
            bool enabledOnly = true,
            string siteName = null)
            where T : CustomTableItem, new()
        {
            T item = this.GetDefaultQuery<T>(
                    columnNames,
                    enabledOnly,
                    siteName: siteName)
                .WhereEquals(
                    nameof(CustomTableItem.ItemGUID), guid).FirstOrDefault();

            return item;
        }

        public List<T> GetCustomTableItemsByGuids<T>(
            List<Guid> guids,
            IEnumerable<string> columnNames = null,
            bool enabledOnly = true,
            string orderBy = null,
            string siteName = null)
            where T : CustomTableItem, new()
        {
            List<T> items = this.GetDefaultQuery<T>(
                    columnNames,
                    enabledOnly,
                    orderBy,
                    siteName)
                .WhereIn(nameof(CustomTableItem.ItemGUID), guids)
                .ToList();

            return items;
        }

        public List<T> GetAllCustomTableItems<T>(
            IEnumerable<string> columnNames = null,
            bool enabledOnly = true,
            string orderBy = null,
            string siteName = null)
            where T : CustomTableItem, new()
        {
            return this.GetDefaultQuery<T>(
                    columnNames,
                    enabledOnly,
                    orderBy,
                    siteName)
                .ToList();
        }

        private ObjectQuery<T> GetDefaultQuery<T>(
            IEnumerable<string> columnNames = null,
            bool enabledOnly = true,
            string orderBy = null,
            string siteName = null)
            where T : CustomTableItem, new()
        {
            var query = CustomTableItemProvider
                .GetItems<T>()
                .OnSite(siteName.ReplaceIfEmpty(this.context.Site?.SiteName));

            if (columnNames != null && columnNames.Any())
            {
                query = query.Columns(columnNames);
            }

            if (enabledOnly && !string.IsNullOrWhiteSpace(this.EnabledColumnName))
            {
                query = query.WhereTrue(this.EnabledColumnName);
            }

            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                query = query.OrderByAscending(orderBy);
            }

            return query;
        }
    }
}
