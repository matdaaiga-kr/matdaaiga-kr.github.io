using System.Text;

using MatdaAIga.LinkConverter.Models;

namespace MatdaAIga.LinkConverter.Services;

/// <summary>
/// This represents the service entity to convert YAML data into markdown text.
/// </summary>
public class ConverterService : IConverterService
{   
    /// <inheritdoc />
    public async Task<LinkCollection> LoadAsync(string? filepath)
    {
        // 구현해야함 : 임시 내용
        return await Task.FromResult(new LinkCollection()).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task<string> ConvertAsync(LinkCollection data)
    {
        var sb = new StringBuilder();

        foreach (var link in data.Links)
        {
            sb.AppendLine(
                string.IsNullOrWhiteSpace(link.ImageUrl)
                    ? $"- [{link.Title}]({link.Url})"
                    : $"- [![{link.Title}]({link.ImageUrl})]({link.Url})\n  [{link.Title}]({link.Url})"
            );
        }

        return await Task.FromResult(sb.ToString().TrimEnd()).ConfigureAwait(false);
    }
    
    /// <inheritdoc />
    public async Task SaveAsync(string? markdown, string? filepath)
    {   
        ArgumentNullException.ThrowIfNullOrWhiteSpace(markdown, nameof(markdown));
        ArgumentNullException.ThrowIfNullOrWhiteSpace(filepath, nameof(filepath));

        var content = await File.ReadAllTextAsync(filepath).ConfigureAwait(false);
        var segment = content.Split([ "<!-- {{ LINKS }} -->" ], StringSplitOptions.RemoveEmptyEntries)
                             .Where(p => string.IsNullOrWhiteSpace(p.Trim()) == false)
                             .Select(p => p.Trim())
                             .ToList();

        if (segment.Count != 2)
        {
            throw new InvalidOperationException("The given file is not properly formatted");
        }

        segment.Insert(1, markdown);

        var merged = string.Join("\n\n<!-- {{ LINKS }} -->\n\n", segment);
        await File.WriteAllTextAsync(filepath, merged).ConfigureAwait(false);
    }
}
