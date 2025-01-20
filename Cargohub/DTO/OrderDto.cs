public class OrderDto
{
    public int Id { get; set; }
    public int? SourceId { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime RequestDate { get; set; }
    public string? Reference { get; set; }
    public string? OrderStatus { get; set; }
    public double? TotalAmount { get; set; }
    public List<OrderStockDto> Items { get; set; } = new List<OrderStockDto>();
}
