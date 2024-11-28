namespace TeddyCourseYT.Helpers;

public class QueryObject
{
    public string? Title { get; set; }
    public int? MinPrice { get; set; }
    public int? MaxPrice { get; set; }

    public string? SortBy { get; set; } = null;

    public bool IsDescending { get; set; } = false;

    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 20;


}