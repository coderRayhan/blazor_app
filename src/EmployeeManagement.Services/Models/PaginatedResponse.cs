namespace EmployeeManagement.Services.Models;

public class PaginatedResponse<T>
{
    public int CurrentPage { get; }
    public int TotalItems { get; set; }
    public int TotalPages { get; set; }
    public bool HasPreviousPage => CurrentPage > 1;
    public bool HasNextPage => CurrentPage < TotalPages;
    public IEnumerable<T> Items { get; set; }
}
