namespace _Scripts.Commands {
    public class SlowCommand : ICommand {
        public void Execute(PlayerController player) {
            player.SetSlow();
        }
    }
}