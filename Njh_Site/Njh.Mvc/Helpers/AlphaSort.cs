using CMS.DocumentEngine;
using Njh.Kernel.Models.Dto;

namespace Njh.Mvc.Helpers
{
    public static class AlphaSort
    {
        public static SortedDictionary<char, List<SimpleLink>> GetAlphaSortedPages(IEnumerable<TreeNode> treeNodes)
        {

            var results = new SortedDictionary<char, List<SimpleLink>>();

            for (char letter = 'A'; letter <= 'Z'; letter++)
            {
                var links = treeNodes.Where(n => n.DocumentName.StartsWith(letter.ToString(), StringComparison.OrdinalIgnoreCase))
                                        .Select(n => new SimpleLink()
                                        {
                                            Text = n.DocumentName,
                                            Link = n.GetBooleanValue("Hide_Url", false) ? string.Empty : n.NodeAliasPath,
                                        })
                                        .ToList();
                results.Add(letter, links);
            }

            return results;
        }

    }
}
