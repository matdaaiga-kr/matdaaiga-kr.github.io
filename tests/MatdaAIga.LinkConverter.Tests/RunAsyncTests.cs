using MatdaAIga.LinkConverter.Controllers;
using MatdaAIga.LinkConverter.Services;
using MatdaAIga.LinkConverter.Models;
using NSubstitute;
using Shouldly;

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
        var tempFilePath = Path.GetTempFileName(); 
        await File.WriteAllTextAsync(tempFilePath, 
        """
        - name: Test
          links:
            - name: Test Link
              url: https://test.com
        """
        ); 

        var args = new[] { "-f", tempFilePath };
        var service = Substitute.For<IConverterService>();
        var controller = new ConverterController(service);

        var linkCollection = new LinkCollection 
        { 
            Name = "Test", 
            Links = new List<LinkItem>
            {
                new LinkItem { Title = "Test Link", Url = "https://test.com" }
            } 
        };
        var markdown = 
        """
        Title: Test
        ---
        - [Test Link](https://test.com)
        ---
        """;
        // TODO: 파일 입출력으로 실제 동작 여부 테스트, Service 구현 후 진행
        // service.LoadAsync(tempFilePath).Returns(Task.FromResult(linkCollection));
        // service.ConvertAsync(linkCollection).Returns(Task.FromResult(markdown));
        // service.SaveAsync(markdown, tempFilePath).Returns(Task.CompletedTask);

        // try
        // {
        //     // Act
        //     await controller.RunAsync(args);

        //     // Assert 
        //     (await service.Received(1).LoadAsync(tempFilePath)).ShouldBe(linkCollection);
        //     (await service.Received(1).ConvertAsync(linkCollection)).ShouldBe(markdown);
        //     await service.Received(1).SaveAsync(markdown, tempFilePath);
        // }
        // finally
        // {
        //     if (File.Exists(tempFilePath))
        //     {
        //         File.Delete(tempFilePath);
        //     }
        // }
    }
}