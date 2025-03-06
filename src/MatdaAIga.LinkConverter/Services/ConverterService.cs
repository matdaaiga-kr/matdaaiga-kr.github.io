using MatdaAIga.LinkConverter.Models;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace MatdaAIga.LinkConverter.Services;

public class ConverterServices: IConverterService
{
    public Task<LinkCollection> LoadAsync(string filepath)
    {
        var yamlText = File.ReadAllText(filepath);
        var filteredYaml = string.Join("\n", yamlText.Skip(2));
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();
        var result = deserializer.Deserialize<LinkCollection>(filteredYaml); 
        Console.WriteLine(result.Name);
        return Task.FromResult(result);
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
            } 
            else
            {
                markdown += $"- [link.Title](link.Url)\n"; 
            }
        }
        return Task.FromResult(markdown);
    }

    public async Task SaveAsync(string markdown, string filepath)
    {
        
    }
}