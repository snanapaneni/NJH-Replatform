using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CMS.DocumentEngine;

namespace Njh.Kernel.Services
{
    public abstract class BaseContentService<TDocument> : ServiceBase
        where TDocument : TreeNode, new()
    {
        protected DocumentQuery<TDocument> GetUncachedItems(string path)
        {
            return DocumentHelper.GetDocuments<TDocument>()
                .OnCurrentSite()
                .CombineWithDefaultCulture()
                .Path(path)
                .Published()
                .LatestVersion()
                .OrderBy("NodeLevel", "NodeOrder");
        }
    }
}
