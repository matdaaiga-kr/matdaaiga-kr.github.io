namespace MatdaAIga.LinkConverter.Controllers;

/// <summary>
/// This provides interfaces th the <see cref="ConverterController"/> class
/// </summary>
public interface IConverterController
{
    /// <summary>
    /// Invokes the conversion processs
    /// </summary>    
    /// <param name="args">The list of arguments passed from the command line</param>
    Task RunAsync(string[] args);
}