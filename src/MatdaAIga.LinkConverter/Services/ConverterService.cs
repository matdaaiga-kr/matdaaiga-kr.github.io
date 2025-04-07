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
