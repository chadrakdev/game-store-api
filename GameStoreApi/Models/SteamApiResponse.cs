namespace GameStoreApi.Models;

public class SteamApiResponse
{
    public Applist Applist { get; set; }
}

public class Applist
{
    public List<App> Apps { get; set; }
}

public class App
{
    public int AppId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string Genre { get; set; }
    public string Price { get; set; }
}