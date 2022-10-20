namespace _Scripts.Commands {
    public class MoveUpCommand : ICommand {
        public void Execute(PlayerController player) {
            player.SetSpeedY(true);
        }
    }
}