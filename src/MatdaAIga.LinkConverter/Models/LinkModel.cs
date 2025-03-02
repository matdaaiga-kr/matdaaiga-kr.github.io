using System.Text.Json.Serialization;

public class LinkCollection
{
    public string Name { get; set; } = string.Empty;
    public List<LinkItem> Links { get; set; } = [];   
}

public class LinkItem
{
    public string Title { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    [JsonPropertyName("image_url")]
    public string? ImageUrl { get; set; }
}