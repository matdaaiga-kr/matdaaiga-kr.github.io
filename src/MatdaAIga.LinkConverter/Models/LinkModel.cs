using System.Text.Json.Serialization;

namespace MatdaAIga.LinkConverter.Models;

/// <summary>
/// This represents the collection entity of the links rendered in the links page.
/// </summary>
public class LinkCollection
{
    /// <summary>
    /// Gets or sets the name of links document
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the collection of <see cref="LinkItem"/> objects
    /// </summary>
    public List<LinkItem> Links { get; set; } = [];   
}

/// <summary>
/// This represents the individual link item entity.
/// </summary>
public class LinkItem
{
    /// <summary>
    /// Gets or sets the title of the link used for the 'title' attribute of the 'a' tag and 'alt' attribute of the 'img' tag
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL of the link used for the 'href' attribute of the 'a' tag. It must start with 'https://"
    /// </summary>
    public string Url { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL of the image used for the 'src' attribute of the 'img' tag. It must start with either '/images/' or 'https://'
    /// </summary>
    [JsonPropertyName("image_url")]
    public string? ImageUrl { get; set; }
}