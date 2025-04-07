using MatdaAIga.LinkConverter.Controllers;
using MatdaAIga.LinkConverter.Services;

public class Program
{
    public static async Task Main(string[] args)
    {
        var controller = new ConverterController(new ConverterService());
        await controller.RunAsync(args);
    }
}
