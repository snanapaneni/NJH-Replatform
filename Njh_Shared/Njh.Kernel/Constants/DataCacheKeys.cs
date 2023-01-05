using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Njh.Kernel.Constants
{
    /// <summary>
    /// Cache keys for caching resulting data, used to enforce consistent cache name scheme.
    /// </summary>
    public static class DataCacheKeys
    {
        /// <summary>
        /// _UniqueName_|bypath|_alias path_|pagetype|_page type code name_.
        /// </summary>
        public const string DataSetByPathByType = "{0}|bypath|{1}|pagetype|{2}";

        /// <summary>
        /// _UniqueName_|bypath|_alias path_|pagetype|_page type code name_.
        /// </summary>
        public const string DataSetByIdByType = "{0}|byid|{1}|pagetype|{2}";

        /// <summary>
        /// _UniqueName_|pagetype|_page type code name_.
        /// </summary>
        public const string DataSetByPageType = "{0}|pagetype|{1}";

        /// <summary>
        /// _UniqueName_|tablename|_tablename_.
        /// </summary>
        public const string DataSetByTableName = "{0}|tablename|{1}";

        /// <summary>
        /// _UniqueName_|_key_.
        /// </summary>
        public const string DataSetByKey = "{0}|{1}";

        /// <summary>
        /// _ProjectCodeName_|CustomTable|ProgramNetworkType|All.
        /// </summary>
        public const string GeneralTypeItemsAll = "{0}|CustomTable|GeneralType|All";

        /// <summary>
        /// _ProjectCodeName_|CustomTable|ProgramNetworkType|All.
        /// </summary>
        public const string ProgramNetworkTypeItemsAll = "{0}|CustomTable|ProgramNetworkType|All";

        /// <summary>
        /// _ProjectCodeName_|CustomTable|Initiative|All.
        /// </summary>
        public const string InitiativeItemsAll = "{0}|CustomTable|Initiative|All";

        /// <summary>
        /// _ProjectCodeName_|CustomTable|Initiative|All.
        /// </summary>
        public const string TopicItemsAll = "{0}|Topics|All";

        /// <summary>
        /// _ProjectCodeName_|CustomTable|ResourceType|All.
        /// </summary>
        public const string ResourceTypeItemsAll = "{0}|CustomTable|ResourceType|All";

        /// <summary>
        /// UserInfo|Email|_email_.
        /// </summary>
        public const string UserByEmail = "UserInfo|Email|{0}";

        /// <summary>
        /// UserInfo|Guid|_guid_.
        /// </summary>
        public const string UserByGuid = "UserInfo|Guid|{0}";

        /// <summary>
        /// UserInfo|Id|_id_.
        /// </summary>
        public const string UserById = "UserInfo|Id|{0}";
    }
}
