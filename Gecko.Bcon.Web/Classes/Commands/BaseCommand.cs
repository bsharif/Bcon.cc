namespace Gecko.Bcon.Web.Classes.Commands {
    /// <summary>
    /// Base class for commands
    /// </summary>
    public abstract class BaseCommand {
        /// <summary>
        /// Default constructor 
        /// </summary>
        public BaseCommand() {
            Result = new CommandResult();
        }

        /// <summary>
        /// The command result
        /// </summary>
        public CommandResult Result { get; set; }

        /// <summary>
        /// Helper class representing the command result
        /// </summary>
        public class CommandResult {
            public string Message { get; set; }
            public bool Successful { get; set; }
        }
    }
}