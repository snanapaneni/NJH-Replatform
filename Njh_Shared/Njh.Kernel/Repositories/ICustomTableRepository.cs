namespace Njh.Kernel.Repositories
{
    using CMS.CustomTables;

    public interface ICustomTableRepository : IRepository
    {
        string EnabledColumnName { get; set; }
        T GetCustomTableItemByGuid<T>(
            Guid guid,
            IEnumerable<string> columnNames = null,
            bool enabledOnly = true,
            string siteName = null)
            where T : CustomTableItem, new();

        List<T> GetCustomTableItemsByGuids<T>(
            List<Guid> guids,
            IEnumerable<string> columnNames = null,
            bool enabledOnly = true,
            string orderBy = null,
            string siteName = null)
            where T : CustomTableItem, new();

        List<T> GetAllCustomTableItems<T>(
            IEnumerable<string> columnNames = null,
            bool enabledOnly = true,
            string orderBy = null,
            string siteName = null)
            where T : CustomTableItem, new();
    }
}
