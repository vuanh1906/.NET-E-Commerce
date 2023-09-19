using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace Skinet.Helpers
{
    public class Pagination<T> where T : class
    {
        public Pagination(int pageIndex, int pageSize, int count, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            Data = data;
            TotalPage = (int)Math.Ceiling(count / (double)pageSize);
        }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public int TotalPage { get; set; }
        public IReadOnlyList<T> Data { get; set; }
    }
}
