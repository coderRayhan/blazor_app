namespace EmployeeManagement.API.Models.Common;

public class PaginationFilter
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SearchText { get; set; }
    public string OrderBy { get; set; } = "Id";
    public string SortingDirection { get; set; } = "DESC";
}
