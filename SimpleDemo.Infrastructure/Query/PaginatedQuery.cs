using System.ComponentModel.DataAnnotations;

namespace SimpleDemo.Infrastructure.Query
{
    public class PaginatedQuery : IQuery
    {
        [Range(1, int.MaxValue)]
        public int PageIndex { get; init; } = 1;

        [Range(10, 100)]
        public int PageSize { get; init; } = 10;
    }
}