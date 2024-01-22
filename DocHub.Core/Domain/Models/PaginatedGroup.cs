using System.Collections;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DocHub.Core.Domain.Models;

public class PaginatedGroup<TGroup, TItem> : IEnumerable<IGrouping<TGroup, TItem>>
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<IGrouping<TGroup, TItem>> Items { get; set; }

    private readonly IComparer<TGroup> _groupComparer;

    private PaginatedGroup(IEnumerable<IGrouping<TGroup, TItem>> items, int pageNumber, int pageSize, IComparer<TGroup> groupComparer)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(items.Count() / (double)pageSize);
        Items = items.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        this._groupComparer = groupComparer;
    }
    public bool HasNextPage => (PageNumber < TotalPages);
    public bool IsEmpty => (!Items.Any());
    public static PaginatedGroup<TGroup, TItem> Create(IEnumerable<TItem> items, Func<TItem, TGroup> keySelector, int pageNumber, int pageSize, IComparer<TGroup> groupComparer = null)
    {
        var groupedItems = items.GroupBy(keySelector).OrderBy(group => group.Key);
        return new PaginatedGroup<TGroup, TItem>(groupedItems, pageNumber, pageSize, groupComparer);
    }

    public IEnumerator<IGrouping<TGroup, TItem>> GetEnumerator()
    {
        return Items.OrderBy(g => g.Key, _groupComparer).GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}