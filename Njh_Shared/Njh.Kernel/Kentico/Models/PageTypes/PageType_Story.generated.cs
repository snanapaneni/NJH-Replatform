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

[assembly: RegisterDocumentType(PageType_Story.CLASS_NAME, typeof(PageType_Story))]

namespace Njh.Kernel.Kentico.Models.PageTypes
{
    /// <summary>
    /// Represents a content item of type PageType_Story.
    /// </summary>
    public partial class PageType_Story : TreeNode
    {
        #region "Constants and variables"

        /// <summary>
        /// The name of the data class.
        /// </summary>
        public const string CLASS_NAME = "NJH.PageType_Story";


        /// <summary>
        /// The instance of the class that provides extended API for working with PageType_Story fields.
        /// </summary>
        private readonly PageType_StoryFields mFields;

        #endregion


        #region "Properties"

        /// <summary>
        /// PageType_PageID.
        /// </summary>
        [DatabaseIDField]
        public int PageType_StoryID
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("PageType_StoryID"), 0);
            }
            set
            {
                SetValue("PageType_StoryID", value);
            }
        }


        /// <summary>
        /// Page Name.
        /// </summary>
        [DatabaseField]
        public string PageName
        {
            get
            {
                return ValidationHelper.GetString(GetValue("PageName"), @"");
            }
            set
            {
                SetValue("PageName", value);
            }
        }


        /// <summary>
        /// Page Title.
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
        /// Categories.
        /// </summary>
        public string Categories1
        {
            get
            {
                return ValidationHelper.GetString(GetValue("Categories"), @"");
            }
            set
            {
                SetValue("Categories", value);
            }
        }


        /// <summary>
        /// Primary Category.
        /// </summary>
        [DatabaseField]
        public int PrimaryCategory
        {
            get
            {
                return ValidationHelper.GetInteger(GetValue("PrimaryCategory"), 0);
            }
            set
            {
                SetValue("PrimaryCategory", value);
            }
        }


        /// <summary>
        /// Use this checkbox to feature pages in the featured sections on Home, Health Insights, Patient Stories, etc.
        /// </summary>
        [DatabaseField]
        public bool FeaturedOnHomePage
        {
            get
            {
                return ValidationHelper.GetBoolean(GetValue("FeaturedOnHomePage"), false);
            }
            set
            {
                SetValue("FeaturedOnHomePage", value);
            }
        }


        /// <summary>
        /// Show on HTLP.
        /// </summary>
        [DatabaseField]
        public bool ShowOnHTLP
        {
            get
            {
                return ValidationHelper.GetBoolean(GetValue("ShowOnHTLP"), false);
            }
            set
            {
                SetValue("ShowOnHTLP", value);
            }
        }


        /// <summary>
        /// Page Blurb.
        /// </summary>
        [DatabaseField]
        public string PageBlurb
        {
            get
            {
                return ValidationHelper.GetString(GetValue("PageBlurb"), @"");
            }
            set
            {
                SetValue("PageBlurb", value);
            }
        }


        /// <summary>
        /// Page Content.
        /// </summary>
        [DatabaseField]
        public string PageContent
        {
            get
            {
                return ValidationHelper.GetString(GetValue("PageContent"), @"");
            }
            set
            {
                SetValue("PageContent", value);
            }
        }


        /// <summary>
        /// Page Image (888x507).
        /// </summary>
        [DatabaseField]
        public string PageImage
        {
            get
            {
                return ValidationHelper.GetString(GetValue("PageImage"), @"");
            }
            set
            {
                SetValue("PageImage", value);
            }
        }


        /// <summary>
        /// Page Image Alt Text.
        /// </summary>
        [DatabaseField]
        public string PageImageAltText
        {
            get
            {
                return ValidationHelper.GetString(GetValue("PageImageAltText"), @"");
            }
            set
            {
                SetValue("PageImageAltText", value);
            }
        }


        /// <summary>
        /// Short Description/Summary.
        /// </summary>
        [DatabaseField]
        public string Summary
        {
            get
            {
                return ValidationHelper.GetString(GetValue("Summary"), @"");
            }
            set
            {
                SetValue("Summary", value);
            }
        }


        /// <summary>
        /// Call To Action (CTA) Title.
        /// </summary>
        [DatabaseField]
        public string CtaTitle
        {
            get
            {
                return ValidationHelper.GetString(GetValue("CtaTitle"), @"");
            }
            set
            {
                SetValue("CtaTitle", value);
            }
        }


        /// <summary>
        /// CTA Link URL.
        /// </summary>
        [DatabaseField]
        public string CtaUrl
        {
            get
            {
                return ValidationHelper.GetString(GetValue("CtaUrl"), @"");
            }
            set
            {
                SetValue("CtaUrl", value);
            }
        }


        /// <summary>
        /// CTA no.2 Title.
        /// </summary>
        [DatabaseField]
        public string Cta2Title
        {
            get
            {
                return ValidationHelper.GetString(GetValue("Cta2Title"), @"");
            }
            set
            {
                SetValue("Cta2Title", value);
            }
        }


        /// <summary>
        /// CTA no.2 URL.
        /// </summary>
        [DatabaseField]
        public string Cta2Url
        {
            get
            {
                return ValidationHelper.GetString(GetValue("Cta2Url"), @"");
            }
            set
            {
                SetValue("Cta2Url", value);
            }
        }


        /// <summary>
        /// CTA no.3 Title.
        /// </summary>
        [DatabaseField]
        public string Cta3Title
        {
            get
            {
                return ValidationHelper.GetString(GetValue("Cta3Title"), @"");
            }
            set
            {
                SetValue("Cta3Title", value);
            }
        }


        /// <summary>
        /// CTA no.3 URL.
        /// </summary>
        [DatabaseField]
        public string Cta3Url
        {
            get
            {
                return ValidationHelper.GetString(GetValue("Cta3Url"), @"");
            }
            set
            {
                SetValue("Cta3Url", value);
            }
        }


        /// <summary>
        /// Sub Header.
        /// </summary>
        [DatabaseField]
        public string SubHeader
        {
            get
            {
                return ValidationHelper.GetString(GetValue("SubHeader"), @"");
            }
            set
            {
                SetValue("SubHeader", value);
            }
        }


        /// <summary>
        /// Story Type.
        /// </summary>
        [DatabaseField]
        public string StoryType
        {
            get
            {
                return ValidationHelper.GetString(GetValue("StoryType"), @"");
            }
            set
            {
                SetValue("StoryType", value);
            }
        }


        /// <summary>
        /// Check this box if you do not want the link in a test directory to link.
        /// </summary>
        [DatabaseField]
        public bool Hide_URL
        {
            get
            {
                return ValidationHelper.GetBoolean(GetValue("Hide_URL"), false);
            }
            set
            {
                SetValue("Hide_URL", value);
            }
        }


        /// <summary>
        /// Reviewed Date.
        /// </summary>
        [DatabaseField]
        public DateTime ReviewedDate
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("ReviewedDate"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("ReviewedDate", value);
            }
        }


        /// <summary>
        /// Reviewed By.
        /// </summary>
        [DatabaseField]
        public string ReviewedBy
        {
            get
            {
                return ValidationHelper.GetString(GetValue("ReviewedBy"), @"");
            }
            set
            {
                SetValue("ReviewedBy", value);
            }
        }


        /// <summary>
        /// Drag to select from listed NJH - Physicians.
        /// </summary>
        [DatabaseField]
        public string ReviewerListGUID
        {
            get
            {
                return ValidationHelper.GetString(GetValue("ReviewerListGUID"), @"");
            }
            set
            {
                SetValue("ReviewerListGUID", value);
            }
        }


        /// <summary>
        /// This field takes precedence over the reviewer list.  It can be used for organizations, departments etc.  It will conditionally link to a URL if provided below.
        /// </summary>
        [DatabaseField]
        public string ReviewerListText
        {
            get
            {
                return ValidationHelper.GetString(GetValue("ReviewerListText"), @"");
            }
            set
            {
                SetValue("ReviewerListText", value);
            }
        }


        /// <summary>
        /// Open link in new window?.
        /// </summary>
        [DatabaseField]
        public bool ReviewerListTextURLTarget
        {
            get
            {
                return ValidationHelper.GetBoolean(GetValue("ReviewerListTextURLTarget"), true);
            }
            set
            {
                SetValue("ReviewerListTextURLTarget", value);
            }
        }


        /// <summary>
        /// Alternative List URL.
        /// </summary>
        [DatabaseField]
        public string ReviewerListTextURL
        {
            get
            {
                return ValidationHelper.GetString(GetValue("ReviewerListTextURL"), @"");
            }
            set
            {
                SetValue("ReviewerListTextURL", value);
            }
        }


        /// <summary>
        /// Content Contact.
        /// </summary>
        [DatabaseField]
        public string Document_Reviewer
        {
            get
            {
                return ValidationHelper.GetString(GetValue("Document_Reviewer"), @"");
            }
            set
            {
                SetValue("Document_Reviewer", value);
            }
        }


        /// <summary>
        /// Review Comments.
        /// </summary>
        [DatabaseField]
        public string Review_Comments
        {
            get
            {
                return ValidationHelper.GetString(GetValue("Review_Comments"), @"");
            }
            set
            {
                SetValue("Review_Comments", value);
            }
        }


        /// <summary>
        /// Last Review Date.
        /// </summary>
        [DatabaseField]
        public DateTime Last_review_date
        {
            get
            {
                return ValidationHelper.GetDateTime(GetValue("Last_review_date"), DateTimeHelper.ZERO_TIME);
            }
            set
            {
                SetValue("Last_review_date", value);
            }
        }


        /// <summary>
        /// Featured this in site search results.
        /// </summary>
        [DatabaseField]
        public bool IsFeaturedInSearchResults
        {
            get
            {
                return ValidationHelper.GetBoolean(GetValue("IsFeaturedInSearchResults"), false);
            }
            set
            {
                SetValue("IsFeaturedInSearchResults", value);
            }
        }


        /// <summary>
        /// Gets an object that provides extended API for working with PageType_Story fields.
        /// </summary>
        [RegisterProperty]
        public PageType_StoryFields Fields
        {
            get
            {
                return mFields;
            }
        }


        /// <summary>
        /// Provides extended API for working with PageType_Story fields.
        /// </summary>
        [RegisterAllProperties]
        public partial class PageType_StoryFields : AbstractHierarchicalObject<PageType_StoryFields>
        {
            /// <summary>
            /// The content item of type PageType_Story that is a target of the extended API.
            /// </summary>
            private readonly PageType_Story mInstance;


            /// <summary>
            /// Initializes a new instance of the <see cref="PageType_StoryFields" /> class with the specified content item of type PageType_Story.
            /// </summary>
            /// <param name="instance">The content item of type PageType_Story that is a target of the extended API.</param>
            public PageType_StoryFields(PageType_Story instance)
            {
                mInstance = instance;
            }


            /// <summary>
            /// PageType_PageID.
            /// </summary>
            public int ID
            {
                get
                {
                    return mInstance.PageType_StoryID;
                }
                set
                {
                    mInstance.PageType_StoryID = value;
                }
            }


            /// <summary>
            /// Page Name.
            /// </summary>
            public string PageName
            {
                get
                {
                    return mInstance.PageName;
                }
                set
                {
                    mInstance.PageName = value;
                }
            }


            /// <summary>
            /// Page Title.
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
            /// Categories.
            /// </summary>
            public string Categories
            {
                get
                {
                    return mInstance.Categories1;
                }
                set
                {
                    mInstance.Categories1 = value;
                }
            }


            /// <summary>
            /// Primary Category.
            /// </summary>
            public int PrimaryCategory
            {
                get
                {
                    return mInstance.PrimaryCategory;
                }
                set
                {
                    mInstance.PrimaryCategory = value;
                }
            }


            /// <summary>
            /// Use this checkbox to feature pages in the featured sections on Home, Health Insights, Patient Stories, etc.
            /// </summary>
            public bool FeaturedOnHomePage
            {
                get
                {
                    return mInstance.FeaturedOnHomePage;
                }
                set
                {
                    mInstance.FeaturedOnHomePage = value;
                }
            }


            /// <summary>
            /// Show on HTLP.
            /// </summary>
            public bool ShowOnHTLP
            {
                get
                {
                    return mInstance.ShowOnHTLP;
                }
                set
                {
                    mInstance.ShowOnHTLP = value;
                }
            }


            /// <summary>
            /// Page Blurb.
            /// </summary>
            public string PageBlurb
            {
                get
                {
                    return mInstance.PageBlurb;
                }
                set
                {
                    mInstance.PageBlurb = value;
                }
            }


            /// <summary>
            /// Page Content.
            /// </summary>
            public string PageContent
            {
                get
                {
                    return mInstance.PageContent;
                }
                set
                {
                    mInstance.PageContent = value;
                }
            }


            /// <summary>
            /// Page Image (888x507).
            /// </summary>
            public string PageImage
            {
                get
                {
                    return mInstance.PageImage;
                }
                set
                {
                    mInstance.PageImage = value;
                }
            }


            /// <summary>
            /// Page Image Alt Text.
            /// </summary>
            public string PageImageAltText
            {
                get
                {
                    return mInstance.PageImageAltText;
                }
                set
                {
                    mInstance.PageImageAltText = value;
                }
            }


            /// <summary>
            /// Short Description/Summary.
            /// </summary>
            public string Summary
            {
                get
                {
                    return mInstance.Summary;
                }
                set
                {
                    mInstance.Summary = value;
                }
            }


            /// <summary>
            /// Call To Action (CTA) Title.
            /// </summary>
            public string CtaTitle
            {
                get
                {
                    return mInstance.CtaTitle;
                }
                set
                {
                    mInstance.CtaTitle = value;
                }
            }


            /// <summary>
            /// CTA Link URL.
            /// </summary>
            public string CtaUrl
            {
                get
                {
                    return mInstance.CtaUrl;
                }
                set
                {
                    mInstance.CtaUrl = value;
                }
            }


            /// <summary>
            /// CTA no.2 Title.
            /// </summary>
            public string Cta2Title
            {
                get
                {
                    return mInstance.Cta2Title;
                }
                set
                {
                    mInstance.Cta2Title = value;
                }
            }


            /// <summary>
            /// CTA no.2 URL.
            /// </summary>
            public string Cta2Url
            {
                get
                {
                    return mInstance.Cta2Url;
                }
                set
                {
                    mInstance.Cta2Url = value;
                }
            }


            /// <summary>
            /// CTA no.3 Title.
            /// </summary>
            public string Cta3Title
            {
                get
                {
                    return mInstance.Cta3Title;
                }
                set
                {
                    mInstance.Cta3Title = value;
                }
            }


            /// <summary>
            /// CTA no.3 URL.
            /// </summary>
            public string Cta3Url
            {
                get
                {
                    return mInstance.Cta3Url;
                }
                set
                {
                    mInstance.Cta3Url = value;
                }
            }


            /// <summary>
            /// Sub Header.
            /// </summary>
            public string SubHeader
            {
                get
                {
                    return mInstance.SubHeader;
                }
                set
                {
                    mInstance.SubHeader = value;
                }
            }


            /// <summary>
            /// Story Type.
            /// </summary>
            public string StoryType
            {
                get
                {
                    return mInstance.StoryType;
                }
                set
                {
                    mInstance.StoryType = value;
                }
            }


            /// <summary>
            /// Check this box if you do not want the link in a test directory to link.
            /// </summary>
            public bool Hide_URL
            {
                get
                {
                    return mInstance.Hide_URL;
                }
                set
                {
                    mInstance.Hide_URL = value;
                }
            }


            /// <summary>
            /// Reviewed Date.
            /// </summary>
            public DateTime ReviewedDate
            {
                get
                {
                    return mInstance.ReviewedDate;
                }
                set
                {
                    mInstance.ReviewedDate = value;
                }
            }


            /// <summary>
            /// Reviewed By.
            /// </summary>
            public string ReviewedBy
            {
                get
                {
                    return mInstance.ReviewedBy;
                }
                set
                {
                    mInstance.ReviewedBy = value;
                }
            }


            /// <summary>
            /// Drag to select from listed NJH - Physicians.
            /// </summary>
            public string ReviewerListGUID
            {
                get
                {
                    return mInstance.ReviewerListGUID;
                }
                set
                {
                    mInstance.ReviewerListGUID = value;
                }
            }


            /// <summary>
            /// This field takes precedence over the reviewer list.  It can be used for organizations, departments etc.  It will conditionally link to a URL if provided below.
            /// </summary>
            public string ReviewerListText
            {
                get
                {
                    return mInstance.ReviewerListText;
                }
                set
                {
                    mInstance.ReviewerListText = value;
                }
            }


            /// <summary>
            /// Open link in new window?.
            /// </summary>
            public bool ReviewerListTextURLTarget
            {
                get
                {
                    return mInstance.ReviewerListTextURLTarget;
                }
                set
                {
                    mInstance.ReviewerListTextURLTarget = value;
                }
            }


            /// <summary>
            /// Alternative List URL.
            /// </summary>
            public string ReviewerListTextURL
            {
                get
                {
                    return mInstance.ReviewerListTextURL;
                }
                set
                {
                    mInstance.ReviewerListTextURL = value;
                }
            }


            /// <summary>
            /// Content Contact.
            /// </summary>
            public string Document_Reviewer
            {
                get
                {
                    return mInstance.Document_Reviewer;
                }
                set
                {
                    mInstance.Document_Reviewer = value;
                }
            }


            /// <summary>
            /// Review Comments.
            /// </summary>
            public string Review_Comments
            {
                get
                {
                    return mInstance.Review_Comments;
                }
                set
                {
                    mInstance.Review_Comments = value;
                }
            }


            /// <summary>
            /// Last Review Date.
            /// </summary>
            public DateTime Last_review_date
            {
                get
                {
                    return mInstance.Last_review_date;
                }
                set
                {
                    mInstance.Last_review_date = value;
                }
            }


            /// <summary>
            /// Featured this in site search results.
            /// </summary>
            public bool IsFeaturedInSearchResults
            {
                get
                {
                    return mInstance.IsFeaturedInSearchResults;
                }
                set
                {
                    mInstance.IsFeaturedInSearchResults = value;
                }
            }
        }

        #endregion


        #region "Constructors"

        /// <summary>
        /// Initializes a new instance of the <see cref="PageType_Story" /> class.
        /// </summary>
        public PageType_Story() : base(CLASS_NAME)
        {
            mFields = new PageType_StoryFields(this);
        }

        #endregion
    }
}