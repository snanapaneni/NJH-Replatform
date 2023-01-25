using CMS.Taxonomy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Njh.Kernel.Helpers
{
    public static class Category
    {
        public static IEnumerable<string> GetCategoriesNamesByGuid(Guid?[] categoriesGuids)
        {
            if (categoriesGuids == null)
            {
                return Array.Empty<string>();
            }

            var strGuidWhere = string.Join(
                                        ",",
                                        categoriesGuids.Select(guid => $"'{guid}'")
                                                        .ToArray());
            var cats = CategoryInfoProvider.GetCategories(
                where: $"CategoryGUID In ({strGuidWhere})",
                orderBy: "CategoryName",
                columns: "CategoryName, CategoryDisplayName")?
                .Select(cat => cat.CategoryName)
                .ToArray();

            if (cats == null || cats.Length == 0)
            {
                return Array.Empty<string>();
            }

            return cats;

        }
    }
}
