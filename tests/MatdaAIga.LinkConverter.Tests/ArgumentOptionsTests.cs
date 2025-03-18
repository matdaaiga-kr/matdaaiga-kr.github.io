using MatdaAIga.LinkConverter.Options;

namespace MatdaAIga.LinkConverter.Tests;

/// <summary>
/// This represents test entity for the <see cref="ArgumentOptions" /> class.
/// </summary>
public class ArgumentOptionsTests
{
    /// <summary>
    /// Tests help options.
    /// </summary>
    [Fact]
    public void DisplayHelpTest()
    {
        // Arrange
        var args = new[] { "-h" };
        // Assert
        var options = ArgumentOptions.Parse(args);
        // Act
        Assert.True(options.Help);
        Assert.Equal(string.Empty, options.Filepath);
    }

    /// <summary>
    /// Tests file path options.
    /// </summary>
    [Fact]
    public void FilepathTest()
    {
        // Arrange
        var args = new[] { "-f", "/path/to/file.yaml" };
        // Act
        var options = ArgumentOptions.Parse(args);
        // Assert
        Assert.False(options.Help);
        Assert.Equal("/path/to/file.yaml", options.Filepath);
    }

    /// <summary>
    /// Tests invalid arguments.
    /// </summary>
    [Fact]
    public void InvalidArgumentsTest()
    {
        // Arrange
        var args = new string[] { };
        // Act
        var options = ArgumentOptions.Parse(args);
        // Assert
        Assert.True(options.Help);
        Assert.Equal(string.Empty, options.Filepath);
    }
}
