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
        var service = new ConverterService();
        var controller = new ConverterController(service);
        var data = await service.LoadAsync(yamlFilepath);
        var expectedContent = await service.ConvertAsync(data);

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
        var service = new ConverterService();
        var controller = new ConverterController(service);
        var data = await service.LoadAsync(yamlFilepath);
        var expectedContent = await service.ConvertAsync(data);

        // Act
        await controller.RunAsync(args);

        // Assert
        var markdownfileContent = await File.ReadAllTextAsync(markdownFilepath);
        markdownfileContent.ShouldContain(expectedContent);
    }
}