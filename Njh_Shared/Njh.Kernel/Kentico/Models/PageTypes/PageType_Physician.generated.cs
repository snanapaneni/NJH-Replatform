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

[assembly: RegisterDocumentType(PageType_Physician.CLASS_NAME, typeof(PageType_Physician))]

namespace Njh.Kernel.Kentico.Models.PageTypes
{
	/// <summary>
	/// Represents a content item of type PageType_Physician.
	/// </summary>
	public partial class PageType_Physician : TreeNode
	{
		#region "Constants and variables"

		/// <summary>
		/// The name of the data class.
		/// </summary>
		public const string CLASS_NAME = "NJH.PageType_Physician";


		/// <summary>
		/// The instance of the class that provides extended API for working with PageType_Physician fields.
		/// </summary>
		private readonly PageType_PhysicianFields mFields;

		#endregion


		#region "Properties"

		/// <summary>
		/// PageType_PhysicianID.
		/// </summary>
		[DatabaseIDField]
		public int PageType_PhysicianID
		{
			get
			{
				return ValidationHelper.GetInteger(GetValue("PageType_PhysicianID"), 0);
			}
			set
			{
				SetValue("PageType_PhysicianID", value);
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
		/// Display Name.
		/// </summary>
		[DatabaseField]
		public string PhysicianDisplayName
		{
			get
			{
				return ValidationHelper.GetString(GetValue("PhysicianDisplayName"), @"");
			}
			set
			{
				SetValue("PhysicianDisplayName", value);
			}
		}


		/// <summary>
		/// First Name.
		/// </summary>
		[DatabaseField]
		public string FirstName
		{
			get
			{
				return ValidationHelper.GetString(GetValue("FirstName"), @"");
			}
			set
			{
				SetValue("FirstName", value);
			}
		}


		/// <summary>
		/// Middle Name.
		/// </summary>
		[DatabaseField]
		public string MiddleName
		{
			get
			{
				return ValidationHelper.GetString(GetValue("MiddleName"), @"");
			}
			set
			{
				SetValue("MiddleName", value);
			}
		}


		/// <summary>
		/// Last Name.
		/// </summary>
		[DatabaseField]
		public string LastName
		{
			get
			{
				return ValidationHelper.GetString(GetValue("LastName"), @"");
			}
			set
			{
				SetValue("LastName", value);
			}
		}


		/// <summary>
		/// Suffix.
		/// </summary>
		[DatabaseField]
		public string Suffix
		{
			get
			{
				return ValidationHelper.GetString(GetValue("Suffix"), @"");
			}
			set
			{
				SetValue("Suffix", value);
			}
		}


		/// <summary>
		/// Email.
		/// </summary>
		[DatabaseField]
		public string Email
		{
			get
			{
				return ValidationHelper.GetString(GetValue("Email"), @"");
			}
			set
			{
				SetValue("Email", value);
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
		/// Gets an object that provides extended API for working with PageType_Physician fields.
		/// </summary>
		[RegisterProperty]
		public PageType_PhysicianFields Fields
		{
			get
			{
				return mFields;
			}
		}


		/// <summary>
		/// Provides extended API for working with PageType_Physician fields.
		/// </summary>
		[RegisterAllProperties]
		public partial class PageType_PhysicianFields : AbstractHierarchicalObject<PageType_PhysicianFields>
		{
			/// <summary>
			/// The content item of type PageType_Physician that is a target of the extended API.
			/// </summary>
			private readonly PageType_Physician mInstance;


			/// <summary>
			/// Initializes a new instance of the <see cref="PageType_PhysicianFields" /> class with the specified content item of type PageType_Physician.
			/// </summary>
			/// <param name="instance">The content item of type PageType_Physician that is a target of the extended API.</param>
			public PageType_PhysicianFields(PageType_Physician instance)
			{
				mInstance = instance;
			}


			/// <summary>
			/// PageType_PhysicianID.
			/// </summary>
			public int ID
			{
				get
				{
					return mInstance.PageType_PhysicianID;
				}
				set
				{
					mInstance.PageType_PhysicianID = value;
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
			/// Display Name.
			/// </summary>
			public string PhysicianDisplayName
			{
				get
				{
					return mInstance.PhysicianDisplayName;
				}
				set
				{
					mInstance.PhysicianDisplayName = value;
				}
			}


			/// <summary>
			/// First Name.
			/// </summary>
			public string FirstName
			{
				get
				{
					return mInstance.FirstName;
				}
				set
				{
					mInstance.FirstName = value;
				}
			}


			/// <summary>
			/// Middle Name.
			/// </summary>
			public string MiddleName
			{
				get
				{
					return mInstance.MiddleName;
				}
				set
				{
					mInstance.MiddleName = value;
				}
			}


			/// <summary>
			/// Last Name.
			/// </summary>
			public string LastName
			{
				get
				{
					return mInstance.LastName;
				}
				set
				{
					mInstance.LastName = value;
				}
			}


			/// <summary>
			/// Suffix.
			/// </summary>
			public string Suffix
			{
				get
				{
					return mInstance.Suffix;
				}
				set
				{
					mInstance.Suffix = value;
				}
			}


			/// <summary>
			/// Email.
			/// </summary>
			public string Email
			{
				get
				{
					return mInstance.Email;
				}
				set
				{
					mInstance.Email = value;
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
		/// Initializes a new instance of the <see cref="PageType_Physician" /> class.
		/// </summary>
		public PageType_Physician() : base(CLASS_NAME)
		{
			mFields = new PageType_PhysicianFields(this);
		}

		#endregion
	}
}