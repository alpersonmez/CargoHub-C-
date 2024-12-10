public class Location
{
    public int Id { get; set; }
    public int WareHouse_Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool isSoftDeleted { get; set; }
}