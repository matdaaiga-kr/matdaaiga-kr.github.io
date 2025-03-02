/// <summary>
/// Interface for a service that handles conversion operations.
/// </summary>
public interface IConverterService
{
    /// <summary>
    /// run the conversion process
    /// </summary>    
    Task RunAsync(string[] args);

    /// <summary>
    /// Loads data from a specified file and deserializes it into an object of type T.
    /// </summary>
    /// <typeparam name="T">the type of object to deserialize the data into</typeparam> 
    /// <param name="path">the path of data source</param>
    /// <returns>A task that resolves to the deserialized object.</returns>
    Task<T> LoadAsync<T>(string path);

    /// <summary>
    /// Seperate the given data into a list of links data and title(Description in links.md)
    /// </summary>
    /// <typeparam name="T">the type of data to be seperated</typeparam>
    /// <param name="data">the data to be seperated</param>
    /// <returns>a tuple containing the title and list of links data</returns>
    Task<(string, List<string>)> SeperateAsync<T>(T data);

    /// <summary>
    /// Converts the given links data into a list of strings, typically for further processing or output.
    /// </summary>
    /// <param name="title">the title data to be converted into markdown format string</param>
    /// <param name="data">the list of links data to be converted into markdown format string</param>
    /// <returns>a tuple containing the converted title and list of strings</returns>
    Task<(string, List<string>)> ConvertAsync(string title, List<string> data);

    /// <summary>
    /// updates the data with the given list of strings
    /// </summary>
    /// <param name="title">title to be saved</param>
    /// <param name="data">data to be saved</param>
    /// <param name="path">the path to the file to save</param>
    Task SaveAsync(string title, List<string> data, string path);
}