namespace Web.Extensions
{
    using System.Collections.Generic;
    using System.Web.Mvc;

    public static class ListExtensions
    {
        public static SelectList ToSelectList<T>(this IEnumerable<T> collection)
        {
            return new SelectList(collection, "Id", "Nombre");
        }

        public static SelectList ToSelectList<T>(this IEnumerable<T> collection, object selectedValue)
        {
            return new SelectList(collection, "Id", "Nombre", selectedValue);
        }

        public static SelectList ToSelectList<T>(this IEnumerable<T> collection,
                             string dataValueField, string dataTextField)
        {
            return new SelectList(collection, dataValueField, dataTextField);
        }

        public static SelectList ToSelectList<T>(this IEnumerable<T> collection,
                             string dataValueField, string dataTextField, string selectedValue)
        {
            return new SelectList(collection, dataValueField, dataTextField, selectedValue);
        }
    }
}