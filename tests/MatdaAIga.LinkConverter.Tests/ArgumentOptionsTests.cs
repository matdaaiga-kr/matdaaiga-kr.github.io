using MatdaAIga.LinkConverter.Options;
using Shouldly;

namespace MatdaAIga.LinkConverter.Tests;

/// <summary>
/// This represents test entity for the <see cref="ArgumentOptions" /> class.
/// </summary>
public class ArgumentOptionsTests
{
    /// <summary>
    /// Tests help options.
    /// </summary>
    [Theory]
    [InlineData(["-h"])]
    [InlineData(["--help"])]
    public void Given_HelpArguments_When_Invoke_DisplayHelp_Then_It_Should_Return_Results(params string[] args)
    {
        // Arrange & Assert
        var options = ArgumentOptions.Parse(args);
        // Act
        options.Help.ShouldBeTrue();
    }

    /// <summary>
    /// Tests file path options.
    /// </summary>
    [Theory]
    [InlineData("-f", "/path/to/file.yaml")]
    [InlineData("--filepath", "/path/to/file.yaml")]
    public void Given_FilePathArguments_When_Invoke_Parse_Then_Filepath_ShouldBeSet(params string[] args)
    {
        // Arrange & Act
        var options = ArgumentOptions.Parse(args);
        // Assert
        options.Filepath.ShouldBe("/path/to/file.yaml");
    }

    /// <summary>
    /// Tests empty arguments.
    /// </summary>
    [Fact]
    public void Given_EmptyArguments_When_Invoke_Parse_Then_Help_ShouldBeTrue()
    {
        // Arrange
        var args = Array.Empty<string>();
        // Act
        var options = ArgumentOptions.Parse(args);
        // Assert
        options.Help.ShouldBeTrue();
    }

    /// <summary>
    /// Tests invalid arguments.
    /// </summary>
    [Fact]
    public void Given_InvalidArguments_When_Invoke_Parse_Then_Help_ShouldBeTrue()
    {
        // Arrange
        var args = new[] { "-t" };
        // Act
        var options = ArgumentOptions.Parse(args);
        // Assert
        options.Help.ShouldBeTrue();
    }
}
