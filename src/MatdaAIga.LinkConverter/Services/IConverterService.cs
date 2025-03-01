public interface IConverterService
{
    Task RunAsync();
    Task LoadAsync();
    Task ConvertAsync();
    Task SaveAsync();
}