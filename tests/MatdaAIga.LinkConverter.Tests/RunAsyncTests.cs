using MatdaAIga.LinkConverter.Controllers;
using MatdaAIga.LinkConverter.Services;

namespace MatdaAIga.LinkConverter.Tests;

/// <summary>
/// This represents test entity for the <see cref="ConverterController" /> class.
/// </summary>
public class RunAsyncTests
{
    [Theory]
    [InlineData("files/testfile-0.yaml", "files/RunAsyncTests-0.md")]
    public async Task Given_ValidArgumentsWithoutImageUrl_When_Invoke_RunAsync_Then_ShouldContainExpectedContent(string yamlFilepath, string markdownFilepath)
    {
        // Arrange
        var args = new[] { "-f", yamlFilepath, "-m", markdownFilepath };
        var controller = new ConverterController(new ConverterService());
        var expectedContent = "- [testfile-0-1](https://www.microsoft.com)\n" +
                        "- [testfile-0-2](https://www.google.com)\n" +
                        "- [testfile-0-3](https://www.amazon.com)";
        // Act
        await controller.RunAsync(args);

        // Assert
        var markdownfileContent = await File.ReadAllTextAsync(markdownFilepath);
        markdownfileContent.ShouldContain(expectedContent);
    }

    [Theory]
    [InlineData("files/testfile-1.yaml", "files/RunAsyncTests-1.md")]
    public async Task Given_ValidArgumentsWithImageUrl_When_Invoke_RunAsync_Then_ShouldContainExpectedContent(string yamlFilepath, string markdownFilepath)
    {
        // Arrange
        var args = new[] { "-f", yamlFilepath, "-m", markdownFilepath };
        var controller = new ConverterController(new ConverterService());
        var expectedContent = "- [![testfile-1-1](https://www.microsoft.com/favicon.ico)](https://www.microsoft.com)\n  [testfile-1-1](https://www.microsoft.com)\n" +
                        "- [![testfile-1-2](https://www.google.com/favicon.ico)](https://www.google.com)\n  [testfile-1-2](https://www.google.com)\n" +
                        "- [![testfile-1-3](https://www.amazon.com/favicon.ico)](https://www.amazon.com)\n  [testfile-1-3](https://www.amazon.com)";

        // Act
        await controller.RunAsync(args);

        // Assert
        var markdownfileContent = await File.ReadAllTextAsync(markdownFilepath);
        markdownfileContent.ShouldContain(expectedContent);
    }
}