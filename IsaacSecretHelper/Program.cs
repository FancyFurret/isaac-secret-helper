using CommandLine;

namespace IsaacSecretHelper
{
    public static class Program
    {
        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<IshOptions>(args).WithParsed(o => new SecretHelper(o).Run());
        }
    }
}