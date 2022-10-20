namespace _Scripts.Commands {
    public class MoveRightCommand : ICommand {
        public void Execute(PlayerController player) {
            player.SetSpeedX(true);
        }
    }
}