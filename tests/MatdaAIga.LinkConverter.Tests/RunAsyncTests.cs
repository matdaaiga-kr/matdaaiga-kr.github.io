using MatdaAIga.LinkConverter.Controllers;
using MatdaAIga.LinkConverter.Services;
using MatdaAIga.LinkConverter.Models;
using Moq;

namespace MatdaAIga.LinkConverter.Tests;

/// <summary>
/// This represents test entity for the <see cref="ConverterController" /> class.
/// </summary>
public class RunAsyncTests
{
    /// <summary>
    /// Tests the RunAsync method.
    /// </summary>
    [Fact]
    public async Task RunAsyncTest()
    {
        // Arrange
        var args = new[] { "-f", "/path/to/file.yaml" };
        var service = new Mock<IConverterService>();
        var controller = new ConverterController(service.Object);

        var linkCollection = new LinkCollection { Name = "Test", Links = new List<LinkItem>() };
        var markdown = "Test Markdown";

        service.Setup(s => s.LoadAsync("/path/to/file.yaml")).ReturnsAsync(linkCollection);
        service.Setup(s => s.ConvertAsync(linkCollection)).ReturnsAsync(markdown);
        service.Setup(s => s.SaveAsync(markdown, "/path/to/file.yaml")).Returns(Task.CompletedTask);

        // Act
        await controller.RunAsync(args);

        // Assert
        service.Verify(p => p.LoadAsync("/path/to/file.yaml"), Times.Once);
        service.Verify(p => p.ConvertAsync(linkCollection), Times.Once);
        service.Verify(p => p.SaveAsync(markdown, "/path/to/file.yaml"), Times.Once);
    }

    /// <summary>
    /// Tests the RunAsync method with help option.
    /// </summary>
    [Fact]
    public async Task RunAsyncWithHelpTest()
    {
        // Arrange
        var args = new[] { "-h" };
        var service = new Mock<IConverterService>();
        var controller = new ConverterController(service.Object);

        // Act
        await controller.RunAsync(args);

        // Assert
        service.Verify(p => p.LoadAsync(It.IsAny<string>()), Times.Never);
        service.Verify(p => p.ConvertAsync(It.IsAny<LinkCollection>()), Times.Never);
        service.Verify(p => p.SaveAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }
}