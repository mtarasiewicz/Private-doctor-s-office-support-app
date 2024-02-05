using System.Collections;
using Microsoft.EntityFrameworkCore;

namespace DocHub.Core.Domain.Models;

public class PaginatedList<T> : IEnumerable<T>
{
    public List<T> Items { get; set; } 
    public int TotalItems { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; private set; }

    public PaginatedList(List<T> items, int pageIndex, int pageSize, int count)
    {
        PageIndex = pageIndex;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        Items = items;
    }

    public bool HasPreviousPage => (PageIndex > 1);
    public bool HasNextPage => (PageIndex < TotalPages);
    public int FirstItemIndex => (PageIndex - 1) * PageSize + 1;
    public int LastItemIndex => Math.Min(PageIndex * PageSize, TotalItems);

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedList<T>(items, pageIndex, pageSize, count);
    }

    IEnumerator<T> IEnumerable<T>.GetEnumerator()
    {
        return Items.GetEnumerator();
    }

    public IEnumerator GetEnumerator()
    {
        return Items.GetEnumerator();
    }
}