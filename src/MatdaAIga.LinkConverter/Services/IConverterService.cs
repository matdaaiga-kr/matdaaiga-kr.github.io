using MatdaAIga.LinkConverter.Models;

namespace MatdaAIga.LinkConverter.Services;

/// <summary>
/// Interface for a service that handles conversion operations.
/// </summary>
public interface IConverterService
{
    /// <summary>
    /// Loads data from a specified file and deserializes it into an object.
    /// </summary>
    /// <param name="filepath">the path of data source</param>
    /// <returns>Returnss <see cref="LinkCollection"/> instance.></returns>
    Task<LinkCollection> LoadAsync(string filepath);

    /// <summary>
    /// Converts the given data into markdown text.
    /// </summary>
    /// <param name="data"><see cref="LinkCollection"/> object</param>
    /// <returns>Returns the markdown text converted from the given data</returns>
    Task<string> ConvertAsync(LinkCollection data);

    /// <summary>
    /// updates the given file with markdown
    /// </summary>
    /// <param name="markdown">Markdown text to be saved</param>
    /// <param name="filepath">the path to the file to save</param>
    Task SaveAsync(string markdown, string filepath);
}