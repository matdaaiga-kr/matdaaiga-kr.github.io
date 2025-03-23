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
    [InlineData("-h")]
    [InlineData("--help")]
    // TODO: Assert 대신 Shouldly 사용, InlineData 적용
    public void Given_HelpArguments_When_Invoke_DisplayHelp_Then_It_Should_Return_Results(string inlineData)
    {
        // Arrange
        var args = new[] { inlineData };
        // Assert
        var options = ArgumentOptions.Parse(args);
        // Act
        options.Help.ShouldBeTrue();
    }

    /// <summary>
    /// Tests file path options.
    /// </summary>
    [Fact]
    public void Given_FilePathArguments_When_Invoke_Parse_Then_Filepath_ShouldBeSet()
    {
        // Arrange
        var args = new[] { "-f", "/path/to/file.yaml" };
        // Act
        var options = ArgumentOptions.Parse(args);
        // Assert
        options.Help.ShouldBeFalse();
        options.Filepath.ShouldBe("/path/to/file.yaml");
    }

    /// <summary>
    /// Tests invalid arguments.
    /// </summary>
    [Fact]
    public void Given_InvalidArguments_When_Invoke_Parse_Then_Help_ShouldBeTrue_And_Filepath_ShouldBeEmpty()
    {
        // Arrange
        var args = new string[] { };
        // Act
        var options = ArgumentOptions.Parse(args);
        // Assert
        options.Help.ShouldBeTrue();
        options.Filepath.ShouldBeEmpty(string.Empty);
    }
}
