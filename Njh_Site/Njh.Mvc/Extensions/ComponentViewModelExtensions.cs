using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Njh.Mvc.Extensions
{

    using System.Diagnostics.CodeAnalysis;
    using Kentico.PageBuilder.Web.Mvc;

    /// <summary>
    /// Implements extension methods for
    /// <see cref="ComponentViewModel"/> objects.
    /// </summary>
    public static class ComponentViewModelExtensions
    {
        /// <summary>
        /// Returns the properties of the widget or the default properties.
        /// </summary>
        /// <typeparam name="TProperties">
        /// The type of the view model properties.
        /// </typeparam>
        /// <param name="componentViewModel">
        /// The component view model.
        /// </param>
        /// <returns>
        /// The properties of the widget, if available.
        /// The default properties of the widget, otherwise.
        /// </returns>
        [return: NotNull]
        public static TProperties GetPropertiesOrDefault<TProperties>(
           this ComponentViewModel<TProperties> componentViewModel)
            where TProperties : class, IWidgetProperties, new()
        {
            return componentViewModel?.Properties ?? new TProperties();
        }
    }

}
