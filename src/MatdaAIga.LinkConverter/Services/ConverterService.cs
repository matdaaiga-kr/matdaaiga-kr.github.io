using MatdaAIga.LinkConverter.Models;

namespace MatdaAIga.LinkConverter.Services
{
    /// <summary>
    /// This represents the service entity to convert YAML data into markdown text.
    /// </summary>
    public class ConverterService : IConverterService
    {   
        /// <inheritdoc />
        public async Task<LinkCollection> LoadAsync(string filepath)
        {
            // 구현해야함 : 임시 내용
            return await Task.FromResult(new LinkCollection());
        }

        /// <inheritdoc />
        public async Task<string> ConvertAsync(LinkCollection data)
        {
            // 구현해야함 : 임시 내용
            return await Task.FromResult(string.Empty);
        }
        
        /// <inheritdoc />
        public async Task SaveAsync(string markdown, string filepath)
        {   
            ArgumentNullException.ThrowIfNullOrWhiteSpace(markdown, nameof(markdown));
            ArgumentNullException.ThrowIfNullOrWhiteSpace(filepath, nameof(filepath));

            var content = await File.ReadAllTextAsync(filepath);
            var splitedContent = content.Split([ "<!-- {{ LINKS }} -->" ], StringSplitOptions.RemoveEmptyEntries);
            var numberOfPlaceholders = splitedContent.Length-1;

            if (numberOfPlaceholders % 2 != 0 || numberOfPlaceholders == 0)
            {
                throw new InvalidOperationException("The number of placeholders is incorrect");
            }

            var contentSections = splitedContent.Where(p => string.IsNullOrWhiteSpace(p.Trim()) == false)
                                        .Select(p => p.Trim())
                                        .ToList();
            contentSections.Insert(1, markdown);

            var mergedContent = string.Join("\n\n<!-- {{ LINKS }} -->\n\n", contentSections);
            await File.WriteAllTextAsync(filepath, mergedContent);
        }
    }
}