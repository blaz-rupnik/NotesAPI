using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotesApp.Helpers
{
    public static class QueryBuildExtensions
    {
        /// <summary>
        /// Appends where statement to existing query
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="isString"></param>
        /// <param name="isFirst"></param>
        /// <returns></returns>
        public static string AppendFilter(string paramName, bool isString, bool isFirst)
        {
            var result = isFirst ? " WHERE" : " AND";
            var paramForQuery = paramName + "Param";

            if (isString)
            {              
                result += " " + paramName + " LIKE CONCAT ('%', @" + paramForQuery + ",'%')";
                return result;
            }
            else
            {
                result += " " + paramName + " = @" + paramForQuery;
                return result;
            }
        }

        /// <summary>
        /// Appends sort clause to existing query
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="isAscending"></param>
        /// <returns></returns>
        public static string AppendSort(string paramName, bool isAscending) {
            var result = " ORDER BY " + paramName;
            return isAscending ? result : result += " DESC";
        }

        /// <summary>
        /// Appends offset and fetch to existing query
        /// </summary>
        /// <param name="sortAdded"></param>
        /// <param name="sortParam"></param>
        /// <returns></returns>
        public static string AppendPaging(bool sortAdded, string sortParam)
        {
            var result = sortAdded ? "" : (" ORDER BY " + sortParam);
            result += " OFFSET @PageSizeParam * (@PageParam - 1) ROWS FETCH NEXT @PageSizeParam ROWS ONLY";
            return result;
        }
    }
}
