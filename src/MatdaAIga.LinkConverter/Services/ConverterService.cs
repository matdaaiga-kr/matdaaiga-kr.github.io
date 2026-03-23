using System.Text;

using MatdaAIga.LinkConverter.Models;

using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace MatdaAIga.LinkConverter.Services;

/// <summary>
/// This represents the service entity to convert YAML data into markdown text.
/// </summary>
public class ConverterService : IConverterService
{   
    /// <inheritdoc />
    public async Task<LinkCollection> LoadAsync(string? filepath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(filepath, nameof(filepath));

        var yamlContent = await File.ReadAllTextAsync(filepath).ConfigureAwait(false);
        var lines = yamlContent.Split(["\r\n", "\n"], StringSplitOptions.None);
        var filteredYaml = string.Join("\n", lines.Skip(2));
        var deserializer = new DeserializerBuilder()
                            .WithNamingConvention(UnderscoredNamingConvention.Instance)
                            .Build();
        var result = deserializer.Deserialize<LinkCollection>(filteredYaml); 
        return result;
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

        return await Task.FromResult(sb.ToString().Trim()).ConfigureAwait(false);
    }
    
    /// <inheritdoc />
    public async Task<string> ConvertEventsAsync(LinkCollection data)
    {
        var sb = new StringBuilder();

        var grouped = data.Links
            .Select(link =>
            {
                var parts = link.Title.Split(' ');
                var year = parts[0].Replace("년", "");
                var month = parts[1].Replace("월", "");
                var name = string.Join(" ", parts.Skip(2));
                return new { Year = year, Month = int.Parse(month), Name = name, Link = link };
            })
            .GroupBy(x => x.Year)
            .OrderByDescending(g => g.Key);

        foreach (var yearGroup in grouped)
        {
            sb.AppendLine($"### {yearGroup.Key}");
            sb.AppendLine();
            sb.AppendLine("| 월 | 행사 |");
            sb.AppendLine("|---:|------|");

            foreach (var item in yearGroup.OrderByDescending(x => x.Month))
            {
                var eventLink = string.IsNullOrWhiteSpace(item.Link.EventUrl) == false
                    ? $"[{item.Name}]({item.Link.EventUrl})"
                    : item.Name;
                sb.AppendLine($"| {item.Month} | {eventLink} |");
            }

            sb.AppendLine();
        }

        return await Task.FromResult(sb.ToString().Trim()).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task SaveAsync(string? markdown, string? filepath)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(markdown, nameof(markdown));
        ArgumentException.ThrowIfNullOrWhiteSpace(filepath, nameof(filepath));

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