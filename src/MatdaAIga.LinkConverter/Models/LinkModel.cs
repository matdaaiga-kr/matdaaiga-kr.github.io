using System.Text.Json.Serialization;

namespace MatdaAIga.LinkConverter.Models;

/// <summary>
/// Description of markdown file with a list of <see cref="LinkItem"/> objects
/// </summary>
public class LinkCollection
{
    /// <summary>
    /// Description of markdown file
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The list of <see cref="LinkItem"/> objects
    /// </summary>
    public List<LinkItem> Links { get; set; } = [];   
}

/// <summary>
/// Represents a link item with title, URL, and image URL(optional)
/// </summary>
public class LinkItem
{
    /// <summary>
    /// The title of the link
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The URL of the link
    /// </summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// The image URL of the link(optional)
    /// </summary>
    [JsonPropertyName("image_url")]
    public string? ImageUrl { get; set; }
}