namespace Njh.Kernel.Constants
{
    /// <summary>
    /// Contains kentico dummy cache dependency keys.
    /// the naming scheme is ObjecttypeParamParam...
    /// </summary>
    public static class DummyCacheKeys
    {
        /// <summary>
        /// node|_site name_|_alias path_|_culture_.
        /// </summary>
        public const string PageSiteNodeAliasCulture = "node|{0}|{1}|{2}";

        /// <summary>
        /// node|_site name_|_alias path_.
        /// </summary>
        public const string PageSiteNodeAlias = "node|{0}|{1}";

        /// <summary>
        /// nodeid|_node id_.
        /// </summary>
        public const string PageNodeId = "nodeid|{0}";

        /// <summary>
        /// documentid|_document id_.
        /// </summary>
        public const string PageDocumentId = "documentid|{0}";

        /// <summary>
        /// documentid|_document id_|attachments.
        /// </summary>
        public const string PageDocumentIdAttachments = "documentid|{0}|attachments";

        /// <summary>
        /// nodes|_site name_|_page type code name_|all.
        /// </summary>
        public const string PagesSitePagetypesAll = "nodes|{0}|{1}|all";

        /// <summary>
        /// nodeguid|_site name_|_node guid_.
        /// </summary>
        public const string PageSiteNodeGuid = "nodeguid|{0}|{1}";

        /// <summary>
        /// node|_site name_|_alias path_|childnodes.
        /// </summary>
        public const string PageSiteNodePathChildren = "node|{0}|{1}|childnodes";

        /// <summary>
        /// _object type_|all.
        /// </summary>
        public const string ObjectAll = "{0}|all";

        /// <summary>
        /// _object type_|byid|_id_.
        /// </summary>
        public const string ObjectById = "{0}|byid|{1}";

        /// <summary>
        /// _object type_|byname|_code name_.
        /// </summary>
        public const string ObjectByCodeName = "{0}|byname|{1}";

        /// <summary>
        /// _object type_|byguid|_guid_.
        /// </summary>
        public const string ObjectByGuid = "{0}|byguid|{1}";

        /// <summary>
        /// customtableitem._codename_|all.
        /// </summary>
        public const string CustomTableItemsAll = "customtableitem.{0}|all";

        /// <summary>
        /// nodeorder.
        /// </summary>
        public const string NodeOrder = "nodeorder";
    }
}
