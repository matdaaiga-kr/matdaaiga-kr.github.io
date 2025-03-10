namespace MatdaAIga.LinkConverter.Options;

public class ArgumentOptions
{
    public bool Help { get; set; } = false;
    public string Filepath { get; set; } = string.Empty;
    public static ArgumentOptions Parse(string[] args)
    {
        var options = new ArgumentOptions();
        if (args.Length < 2)
        {
            options.Help = true;
            return options;
        }

        for (var i = 0; i < args.Length; i++)
        {
            var arg = args[i];
            switch (arg)
            {
                case "-f":
                case "--filepath":
                    if (i < args.Length - 1)
                    {
                        options.Filepath = args[++i];
                    }
                    break;

                case "-h":
                case "--help":
                    options.Help = true;
                    break;
            }
        }

        return options;
    }
}