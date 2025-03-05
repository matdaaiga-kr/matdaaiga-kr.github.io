using System.Text.RegularExpressions;
using MatdaAIga.LinkConverter.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace MatdaAIga.LinkConverter.Services;

public class ConverterServices: IConverterService
{
    public Task<LinkCollection> LoadAsync(string filepath)
    {
        var yamlText = File.ReadAllText(filepath);
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        var config = deserializer.Deserialize<LinkCollection>(yamlText); 
        return Task.FromResult(config);
    }

    public Task<string> ConvertAsync(LinkCollection data)
    {
        string markdown = "";
        markdown += $"'{data.Name}'\n";
        foreach (var link in data.Links)
        {
            if (link.ImageUrl != null)
            {
                markdown += $"- [![link.Title](link.ImageUrl)](link.Url)<br>\n  [link.Title](link.Url)\n";
            } else{
                markdown += $"- [link.Title](link.Url)\n"; 
            }
        }
        return Task.FromResult(markdown);
    }

    public async Task SaveAsync(string markdown, string filepath)
    {
        if (!File.Exists(filepath))
        {
            throw new FileNotFoundException("File not found", filepath);
        }
        string fileContent = await File.ReadAllTextAsync(filepath);
        string name = markdown.Split('\n')[0];
        fileContent = Regex.Replace(fileContent, @"(?<=^Description:\s*).*", name, RegexOptions.Multiline);
        string links = Regex.Replace(markdown, @"^.*\r?\n?", "");
        fileContent = Regex.Replace(fileContent, @"(?<=---\n)(.*?)(?=\n---)", links, RegexOptions.Singleline);
        await File.WriteAllTextAsync(filepath, fileContent);
    }
}