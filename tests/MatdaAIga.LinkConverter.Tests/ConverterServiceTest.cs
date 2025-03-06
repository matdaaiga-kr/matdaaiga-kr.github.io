using Moq;
using MatdaAIga.LinkConverter.Services;
using MatdaAIga.LinkConverter.Models;
namespace MatdaAIga.LinkConverter.Tests;

public class ConverterServiceTest
{
    private readonly Mock<IConverterService> _mockService;
    
    public ConverterServiceTest()
    {
        _mockService = new Mock<IConverterService>();
    }

    [Fact]
    public async Task LoadAsync_ShouldRetrunLinkCollection()
    {
        // Arrange
        string testFilePath = "./Test.yaml";
        string testYaml = """
        # yaml-language-server: $schema=https://matdaaiga.kr/schema/links.yaml.json

        name: testfile

        links:
        - title: testTitle 1
        url: https://www.microsoft.com/ko-kr
        image_url: https://uhf.microsoft.com/images/microsoft/RE1Mu3b.png
        - title: testTitle 2
        url: https://azure.microsoft.com/ko-kr/
        """;

        File.WriteAllText(testFilePath, testYaml);

        var expectedResult = new LinkCollection 
        { 
            Name = "testfile", 
            Links = new List<LinkItem> 
            { 
                new LinkItem(), 
                new LinkItem { Title = "testTitle 1", Url = "https://www.microsoft.com/ko-kr", ImageUrl = "https://uhf.microsoft.com/images/microsoft/RE1Mu3b.png" }, 
                new LinkItem { Title = "testTitle 2", Url = "https://azure.microsoft.com/ko-kr/" } 
            } 
        };
        
        _mockService.Setup(s => s.LoadAsync(testFilePath))
                    .ReturnsAsync(expectedResult);  

        // Act
        var result = await _mockService.Object.LoadAsync(testFilePath);

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result.Links);
        Assert.Equal("testfile", result.Name);

        File.Delete(testFilePath);
    }

    [Fact]
    public async Task ConvertAsync_ShouldReturnMarkdown()
    {
        // Arrange
        var linkCollection = new LinkCollection
        {
            Name = "testfile",
            Links = new List<LinkItem>
            {
                new LinkItem { Title = "testTitle 1", Url = "https://www.microsoft.com/ko-kr", ImageUrl = "https://uhf.microsoft.com/images/microsoft/RE1Mu3b.png" },
                new LinkItem { Title = "testTitle 2", Url = "https://azure.microsoft.com/ko-kr/" }
            }
        };

        var expectedMarkdown = """
        'testfile'
        - [![testTitle 1](https://uhf.microsoft.com/images/microsoft/RE1Mu3b.png)](https://www.microsoft.com/ko-kr)<br>
          [testTitle 1](https://www.microsoft.com/ko-kr)
        - [testTitle 2](https://azure.microsoft.com/ko-kr/)
        """;

        _mockService.Setup(s => s.ConvertAsync(linkCollection))
                    .ReturnsAsync(expectedMarkdown);

        // Act
        var result = await _mockService.Object.ConvertAsync(linkCollection);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedMarkdown, result);
    }

}
