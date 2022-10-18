namespace _Scripts.Commands {
    public class MoveLeftCommand : ICommand {
        public void Execute(PlayerController player) {
            player.SetSpeedY(false);
        }
    }
}