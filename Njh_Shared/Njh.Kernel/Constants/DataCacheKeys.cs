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
        /// _UniqueName_|bypath|_alias path_|pagetype|_page type code name_|maxdepth|_max nesting level of query_.
        /// </summary>
        public const string DataSetByPathByTypeAndDepth = "{0}|bypath|{1}|pagetype|{2}|maxdepth|{3}";

        /// <summary>
        /// _UniqueName_|byid|_alias path_|pagetype|_page type code name_.
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
        /// PressGaneyPerson|_key_.
        /// </summary>
        public const string PressGaneyPersonKey = "PressGaneyPerson|{0}";

        /// <summary>
        /// PressGaneyAuthToken
        /// </summary>
        public const string PressGaneyTokenKey = "PressGaneyAuthToken";
    }
}
