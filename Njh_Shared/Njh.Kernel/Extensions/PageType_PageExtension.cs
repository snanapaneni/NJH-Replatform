//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Njh.Kernel.Common;
//using Njh.Kernel.Models.Dto;

//namespace Njh.Kernel.Extensions
//{
//    public class PageTypeExtension
//    {
//        /// <summary>
//        /// Returns the value of the field of the document
//        /// or the default value.
//        /// </summary>
//        /// <typeparam name="TValue">
//        /// The type of the field.
//        /// </typeparam>
//        /// <param name="document">
//        /// The document.
//        /// </param>
//        /// <param name="fieldName">
//        /// The field name.
//        /// </param>
//        /// <param name="defaultValue">
//        /// The default value.
//        /// </param>
//        /// <returns>
//        /// The value of the field or the default value.
//        /// </returns>
//        public static Reviewer GetReviewer(
//            this PageType page)
//        {
//            var reviewer = new Reviewer();



//            return new Reviewer();
//            //return
//            //    document.TryGetValue(
//            //        fieldName,
//            //        out object? rawValue) &&
//            //        rawValue is TValue value
//            //    ? value
//            //    : defaultValue;
//        }
//    }
//}
