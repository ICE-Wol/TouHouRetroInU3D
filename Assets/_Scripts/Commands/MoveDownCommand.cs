namespace _Scripts.Commands {
    public class MoveDownCommand : ICommand {
        public void Execute(PlayerController player) {
            player.SetSpeedY(false);
        }
    }
}