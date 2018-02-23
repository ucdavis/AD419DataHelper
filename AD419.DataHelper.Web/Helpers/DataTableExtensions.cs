using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AD419.DataHelper.Web.Helpers
{
    public static class DataTableExtensions
    {
        public static IEnumerable<DataRow> ToEnumerable(this DataRowCollection collection)
        {
            return 
                from DataRow row in collection
                select row;
        }
    }
}