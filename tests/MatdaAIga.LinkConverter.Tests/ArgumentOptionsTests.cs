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
    [Theory]
    [InlineData("-h")]
    [InlineData("--help")]
    public void Given_HelpArguments_When_Invoke_DisplayHelp_Then_It_Should_Return_Results(params string[] args)
    {
        // Arrange & Assert
        var options = ArgumentOptions.Parse(args); 
        // Act
        options.Help.ShouldBeTrue();
    }

    /// <summary>
    /// Tests YAML file path options.
    /// </summary>
    [Theory]
    [InlineData("-f", "/path/to/file.yaml")]
    [InlineData("--filepath", "/path/to/file.yaml")]
    public void Given_YamlFilePathArguments_When_Invoke_Parse_Then_Filepath_ShouldBeSet(params string[] args)
    {
        // Arrange & Act
        var options = ArgumentOptions.Parse(args);
        // Assert
        options.YamlFilepath.ShouldBe(args[1]);
    }

    /// <summary>
    /// Tests Markdown file path options.
    /// </summary>
    [Theory]
    [InlineData("-m", "/path/to/file.md")]
    [InlineData("--markdown", "/path/to/file.md")]
    public void Given_MarkdownFilePathArguments_When_Invoke_Parse_Then_Filepath_ShouldBeSet(params string[] args)
    {

        var options = ArgumentOptions.Parse(args);

        options.MarkdownFilePath.ShouldBe(args[1]);
    }


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
