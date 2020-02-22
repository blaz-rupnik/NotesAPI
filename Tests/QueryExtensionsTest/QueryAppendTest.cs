using NotesApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.QueryExtensionsTest
{
    public class QueryAppendTest
    {
        [Fact]
        public void CheckString_Filter_String_NotFirst()
        {
            var result = QueryBuildExtensions.AppendFilter("Test", true, false);

            Assert.Equal(" AND Test LIKE CONCAT ('%', @TestParam,'%')", result);
        }

        [Fact]
        public void CheckString_Filter_String_First()
        {
            var result = QueryBuildExtensions.AppendFilter("Test", true, true);

            Assert.Equal(" WHERE Test LIKE CONCAT ('%', @TestParam,'%')", result);
        }

        [Fact]
        public void CheckString_Filter_NotString_NotFirst()
        {
            var result = QueryBuildExtensions.AppendFilter("Test", false, false);

            Assert.Equal(" AND Test = @TestParam", result);
        }

        [Fact]
        public void CheckString_Filter_NotString_First()
        {
            var result = QueryBuildExtensions.AppendFilter("Test", false, true);

            Assert.Equal(" WHERE Test = @TestParam", result);
        }

        [Fact]
        public void CheckString_Sort_IsAscending()
        {
            var result = QueryBuildExtensions.AppendSort("Test", true);

            Assert.Equal(" ORDER BY Test", result);
        }

        [Fact]
        public void CheckString_Sort_IsDescending()
        {
            var result = QueryBuildExtensions.AppendSort("Test", false);

            Assert.Equal(" ORDER BY Test DESC", result);
        }

        [Fact]
        public void CheckString_Paging_SortAdded()
        {
            var result = QueryBuildExtensions.AppendPaging(true, "");

            Assert.Equal(" OFFSET @PageSizeParam * (@PageParam - 1) ROWS FETCH NEXT @PageSizeParam ROWS ONLY", result);
        }

        [Fact]
        public void CheckString_Paging_SortNotAdded()
        {
            var result = QueryBuildExtensions.AppendPaging(false, "Test");

            Assert.Equal(" ORDER BY Test OFFSET @PageSizeParam * (@PageParam - 1) ROWS FETCH NEXT @PageSizeParam ROWS ONLY", result);
        }
    }
}
