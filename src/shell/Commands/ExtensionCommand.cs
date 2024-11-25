using McMaster.Extensions.CommandLineUtils;
namespace EpicChainShell.Commands
{
    [Command("extension", Description = "Commands to manage EpicChain shell extensions")]
    [Subcommand(typeof(List), typeof(Register), typeof(Unregister))]
    partial class ExtensionCommand
    {
        internal int OnExecute(CommandLineApplication app, IConsole console)
        {
            console.WriteLine("You must specify at a subcommand.");
            app.ShowHelp(false);
            return 1;
        }
    }
}
