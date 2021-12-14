using System.ComponentModel.DataAnnotations;

namespace Models.Queries;

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