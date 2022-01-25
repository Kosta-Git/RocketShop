using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models.Queries;

public class PageQuery
{
    [Required] [Range(0, int.MaxValue)] public int PageNumber { get; set; } = 0;

    [Required] [Range( 1, 10000 )] public int PageSize { get; set; } = 10;
}