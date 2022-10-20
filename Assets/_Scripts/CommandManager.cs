using System;
using UnityEngine;

namespace _Scripts.Commands {
    public enum Command{
        Empty,
        MoveUp,
        MoveDown,
        MoveRight,
        MoveLeft,
        Slow,
        Fire,
        Bomb,
        Special,
    }
    public class CommandManager : MonoBehaviour {
        private ICommand[] _commands;
        [SerializeField] private PlayerController player;
        [SerializeField] protected KeyCode[] keySettings;

        private void Start() {
            _commands = new ICommand[9];
            for (int i = 0; i <= 8; i++) {
                _commands[i] = CommandFactory.Create(i);
            }
        }

        private void Update() { 
            HandleInput();
        }

        private void HandleInput() {
            for (int i = 0; i <= 8; i++) {
                if (Input.GetKey(keySettings[i])) {
                    _commands[i].Execute(player);
                }
            }
        }
    }
}

/*protected ICommand MoveUp;
protected ICommand MoveDown;
protected ICommand MoveRight;
protected ICommand MoveLeft;
protected ICommand Slow;
protected ICommand Fire;
protected ICommand Bomb;
protected ICommand Special;
protected ICommand EmptyCommand;*/
/*MoveUp = CommandFactory.Create(0);
MoveDown = CommandFactory.Create(1);
MoveRight = CommandFactory.Create(2);
MoveLeft = CommandFactory.Create(3);
Slow = CommandFactory.Create(4);
Fire = CommandFactory.Create(5);
Bomb = CommandFactory.Create(6);
Special = CommandFactory.Create(7);
EmptyCommand = CommandFactory.Create(default);*/