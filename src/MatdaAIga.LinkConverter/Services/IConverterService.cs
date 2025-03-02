/// <summary>
/// Interface for a service that handles conversion operations.
/// </summary>
public interface IConverterService
{
    /// <summary>
    /// Loads data from a specified file and deserializes it into an object.
    /// </summary>
    /// <typeparam name="LinkCollection">the type of object to deserialize the data into</typeparam> 
    /// <param name="filepath">the path of data source</param>
    /// <returns>A task that resolves to the deserialized object.</returns>
    Task<LinkCollection> LoadAsync(string filepath);

    /// <summary>
    /// Converts the given data into a list of markdown format strings.
    /// </summary>
    /// <param name="data">the data object to be converted into markdown format strings</param>
    /// <returns>a list containing the markdown strings converted </returns>
    Task<List<string>> ConvertAsync(LinkCollection data);

    /// <summary>
    /// updates the data with the given list of strings
    /// </summary>
    /// <param name="data">data to be saved</param>
    /// <param name="filepath">the path to the file to save</param>
    Task SaveAsync(List<string> data, string filepath);
}