namespace MatdaAIga.LinkConverter.Controllers;

/// <summary>
/// Interface for a controller that handles conversion operations.
/// </summary>
public interface IConverterController
{
    /// <summary>
    /// Invokes the conversion processs
    /// </summary>    
    /// <param name="args">The list of arguments passed from the command line</param>
    Task RunAsync(string[] args);
}