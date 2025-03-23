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
            var segment = content.Split([ "<!-- {{ LINKS }} -->" ], StringSplitOptions.RemoveEmptyEntries)
                     .Where(p => string.IsNullOrWhiteSpace(p.Trim()) == false)
                     .Select(p => p.Trim())
                     .ToList();

            if (segment.Count != 2)
            {
                throw new InvalidOperationException("The given file is not properly formatted");
            }

            segment.Insert(1, markdown);

            var merged = string.Join("\n\n<!-- {{ LINKS }} -->\n\n", segment);
            await File.WriteAllTextAsync(filepath, merged);
        }
    }
}