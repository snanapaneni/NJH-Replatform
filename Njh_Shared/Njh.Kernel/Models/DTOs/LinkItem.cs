namespace Njh.Kernel.Models.DTOs
{
    /// <summary>
    /// Represents a tree node.
    /// </summary>
    public class LinkItem
    {
        /// <summary>
        /// Gets or sets the display title.
        /// </summary>
        public string DisplayTitle { get; set; }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets the linked page path.
        /// </summary>
        public string LinkedPagePath { get; set; }

        /// <summary>
        /// Gets or sets the node ID.
        /// </summary>
        public int NodeID { get; set; }

        /// <summary>
        /// Gets or sets the node parent ID.
        /// </summary>
        public int NodeParentID { get; set; }

        /// <summary>
        /// Gets or sets the node level.
        /// </summary>
        public int NodeLevel { get; set; }

        /// <summary>
        /// Gets or sets the node order.
        /// </summary>
        public int NodeOrder { get; set; }

        /// <summary>
        /// Gets or sets the node alias path.
        /// </summary>
        public string NodeAliasPath { get; set; }
    }
}