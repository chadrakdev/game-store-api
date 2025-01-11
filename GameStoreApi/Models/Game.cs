namespace GameStoreApi.Models;

public class Game
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string Genre { get; set; }
    public string Price { get; set; }
}