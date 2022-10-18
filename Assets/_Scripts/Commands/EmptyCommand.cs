namespace _Scripts.Commands {
    /// <summary>
    /// Dealing with the keys which doesnt have a corresponding command.
    /// </summary>
    public class EmptyCommand : ICommand {
        public void Execute(PlayerController player) { }
    }
}