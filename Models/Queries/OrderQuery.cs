using Models.Enums;

namespace Models.Queries;

public class OrderQuery : PageQuery
{
    public List<Status> Status { get; set; } = new() { Enums.Status.Pending };
}