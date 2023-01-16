﻿//--------------------------------------------------------------------------------------------------
// <auto-generated>
//
//     This code was generated by code generator tool.
//
//     To customize the code use your own partial class. For more info about how to use and customize
//     the generated code see the documentation at https://docs.xperience.io/.
//
// </auto-generated>
//--------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using CMS;
using CMS.Base;
using CMS.Helpers;
using CMS.DataEngine;
using CMS.DocumentEngine;
using Njh.Kernel.Kentico.Models.PageTypes;

[assembly: RegisterDocumentType(PageType_CTAItem.CLASS_NAME, typeof(PageType_CTAItem))]

namespace Njh.Kernel.Kentico.Models.PageTypes
{
    /// <summary>
    /// Represents a content item of type PageType_CTAItem.
    /// </summary>
    public partial class PageType_CTAItem : TreeNode
    {
        #region "Constants and variables"

        /// <summary>
        /// The name of the data class.
        /// </summary>
        public const string CLASS_NAME = "NJH.PageType_CTAItem";


        /// <summary>
        /// The instance of the class that provides extended API for working with PageType_CTAItem fields.
        /// </summary>
        private readonly PageType_CTAItemFields mFields;

        #endregion


        #region "Properties"

        /// <summary>
        /// PageType_CTAItemID.
        /// </summary>
        [DatabaseIDField]
        public int PageType_CTAItemID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("PageType_CTAItemID"), 0);
            }
            set
            {
                SetValue("PageType_CTAItemID", value);
            }
        }


        /// <summary>
        /// Title.
        /// </summary>
        [DatabaseField]
        public string Title
        {
            get
            {
                return ValidationHelper.GetString(GetValue("Title"), @"");
            }
            set
            {
                SetValue("Title", value);
            }
        }


        /// <summary>
        /// Display Title.
        /// </summary>
        [DatabaseField]
        public string DisplayTitle
        {
            get
            {
                return ValidationHelper.GetString(GetValue("DisplayTitle"), @"");
            }
            set
            {
                SetValue("DisplayTitle", value);
            }
        }


        /// <summary>
        /// A one or two line description of the page.
        /// </summary>
        [DatabaseField]
        public string ShortDescription
        {
            get
            {
                return ValidationHelper.GetString(GetValue("ShortDescription"), @"");
            }
            set
            {
                SetValue("ShortDescription", value);
            }
        }


        /// <summary>
        /// Image (300 x 225).
        /// </summary>
        [DatabaseField]
        public string Image
        {
            get
            {
                return ValidationHelper.GetString(GetValue("Image"), @"");
            }
            set
            {
                SetValue("Image", value);
            }
        }


        /// <summary>
        /// Image Alt Text.
        /// </summary>
        [DatabaseField]
        public string ImageAltText
        {
            get
            {
                return ValidationHelper.GetString(GetValue("ImageAltText"), @"");
            }
            set
            {
                SetValue("ImageAltText", value);
            }
        }


        /// <summary>
        /// Link Page.
        /// </summary>
        [DatabaseField]
        public Guid LinkPage
        {
            get
            {
                return ValidationHelper.GetGuid(GetValue("LinkPage"), Guid.Empty);
            }
            set
            {
                SetValue("LinkPage", value);
            }
        }

        /// <summary>
        /// Link Type.
        /// </summary>
        [DatabaseField]
        public string CssClass
        {
            get
            {
                return ValidationHelper.GetString(GetValue("CssClass"), @"");
            }
            set
            {
                SetValue("CssClass", value);
            }
        }


        /// <summary>
        /// Gets an object that provides extended API for working with PageType_CTAItem fields.
        /// </summary>
        [RegisterProperty]
        public PageType_CTAItemFields Fields
        {
            get
            {
                return mFields;
            }
        }


        /// <summary>
        /// Provides extended API for working with PageType_CTAItem fields.
        /// </summary>
        [RegisterAllProperties]
        public partial class PageType_CTAItemFields : AbstractHierarchicalObject<PageType_CTAItemFields>
        {
            /// <summary>
            /// The content item of type PageType_CTAItem that is a target of the extended API.
            /// </summary>
            private readonly PageType_CTAItem mInstance;


            /// <summary>
            /// Initializes a new instance of the <see cref="PageType_CTAItemFields" /> class with the specified content item of type PageType_CTAItem.
            /// </summary>
            /// <param name="instance">The content item of type PageType_CTAItem that is a target of the extended API.</param>
            public PageType_CTAItemFields(PageType_CTAItem instance)
            {
                mInstance = instance;
            }


            /// <summary>
            /// PageType_CTAItemID.
            /// </summary>
            public int ID
            {
                get
                {
                    return mInstance.PageType_CTAItemID;
                }
                set
                {
                    mInstance.PageType_CTAItemID = value;
                }
            }


            /// <summary>
            /// Title.
            /// </summary>
            public string Title
            {
                get
                {
                    return mInstance.Title;
                }
                set
                {
                    mInstance.Title = value;
                }
            }


            /// <summary>
            /// Display Title.
            /// </summary>
            public string DisplayTitle
            {
                get
                {
                    return mInstance.DisplayTitle;
                }
                set
                {
                    mInstance.DisplayTitle = value;
                }
            }


            /// <summary>
            /// A one or two line description of the page.
            /// </summary>
            public string ShortDescription
            {
                get
                {
                    return mInstance.ShortDescription;
                }
                set
                {
                    mInstance.ShortDescription = value;
                }
            }


            /// <summary>
            /// Image (300 x 225).
            /// </summary>
            public string Image
            {
                get
                {
                    return mInstance.Image;
                }
                set
                {
                    mInstance.Image = value;
                }
            }


            /// <summary>
            /// Image Alt Text.
            /// </summary>
            public string ImageAltText
            {
                get
                {
                    return mInstance.ImageAltText;
                }
                set
                {
                    mInstance.ImageAltText = value;
                }
            }


            /// <summary>
            /// Link Page.
            /// </summary>
            public Guid LinkPage
            {
                get
                {
                    return mInstance.LinkPage;
                }
                set
                {
                    mInstance.LinkPage = value;
                }
            }

            /// <summary>
            /// Link Type.
            /// </summary>
            public string CssClass
            {
                get
                {
                    return mInstance.CssClass;
                }
                set
                {
                    mInstance.CssClass = value;
                }
            }
        }

        #endregion


        #region "Constructors"

        /// <summary>
        /// Initializes a new instance of the <see cref="PageType_CTAItem" /> class.
        /// </summary>
        public PageType_CTAItem() : base(CLASS_NAME)
        {
            mFields = new PageType_CTAItemFields(this);
        }

        #endregion
    }
}