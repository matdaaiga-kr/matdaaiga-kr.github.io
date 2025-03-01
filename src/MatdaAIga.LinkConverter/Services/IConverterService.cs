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
    /// load the data and return it as an object
    /// </summary>
    /// <param name="path">the path to the file to load</param>
    /// <returns>an object from data</returns>
    Task<T> LoadAsync<T>(string path);

    /// <summary>
    /// convert the data to a list of strings
    /// </summary>
    /// <param name="data">the data to convert</param>
    /// <returns>a list of strings representing the converted data</returns>
    Task<List<string>> ConvertAsync<T>(T data);
    

    /// <summary>
    /// save the data to a file
    /// </summary>
    /// <param name="path">the path to the file to save</param>
    Task SaveAsync(string path);
}