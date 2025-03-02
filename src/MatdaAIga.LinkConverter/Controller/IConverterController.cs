/// <summary>
/// Interface for a controller that handles conversion operations.
/// </summary>
public interface IConverterController
{
    /// <summary>
    /// Invokes the conversion processs
    /// </summary>    
    /// <param name="args">The path of the file to convert</param>
    Task RunAsync(string[] args);
}