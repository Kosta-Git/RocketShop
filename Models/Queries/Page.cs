using System.ComponentModel.DataAnnotations;

namespace Models.Queries;

public static class Page
{
    public static Page<Tout> Convert<Tin, Tout>(Page<Tin> page, Func<Tin, Tout> convertFunc)
    {
        return new Page<Tout>
        {
            Data        = page.Data.Select(convertFunc),
            PageNumber  = page.PageNumber,
            PageSize    = page.PageSize,
            TotalPages  = page.TotalPages,
            TotalValues = page.TotalValues,
            NextPage    = page.NextPage
        };
    }
}

public class Page<T>
{
    public IEnumerable<T> Data { get; set; }

    [Range(0, int.MaxValue)]
    public int PageNumber { get; set; }

    [Range(0, int.MaxValue)]
    public int PageSize { get; set; }

    [Range(0, int.MaxValue)] 
    public int TotalPages { get; set; }

    [Range(0, int.MaxValue)]
    public int TotalValues { get; set; }

    [Range(0, int.MaxValue)]
    public int? NextPage { get; set; }
}