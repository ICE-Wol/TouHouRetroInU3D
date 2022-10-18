using UnityEngine;

namespace _Scripts.Commands {
    public static class CommandFactory {
        public static ICommand Create(int ord) {
            switch (ord) {
                case 0: return new EmptyCommand(); 
                case 1: return new MoveUpCommand();
                case 2: return new MoveDownCommand();
                case 3: return new MoveRightCommand();
                case 4: return new MoveLeftCommand();
                case 5: return new SlowCommand();
                case 6: return new FireCommand();
                case 7: return new BombCommand();
                case 8: return new SpecialCommand();
                default: return null;
                //throw new System.IndexOutOfRangeException("Error KeyCode.");
            }
        }
    }
}