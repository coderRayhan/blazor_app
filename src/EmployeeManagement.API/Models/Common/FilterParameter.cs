namespace EmployeeManagement.API.Models.Common;

internal sealed class FilterParameter
{
    public string? SearchText { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
