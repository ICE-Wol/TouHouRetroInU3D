using _Scripts.Commands;

namespace _Scripts {
    public interface ICommand {
        void Execute(PlayerController player);
    }
}