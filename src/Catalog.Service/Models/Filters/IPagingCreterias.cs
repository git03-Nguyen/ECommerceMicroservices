namespace Catalog.Service.Models.Filters;

public interface IPagingCreterias
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}