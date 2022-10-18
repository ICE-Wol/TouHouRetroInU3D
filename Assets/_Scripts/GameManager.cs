using UnityEngine;

namespace _Scripts {
    public class GameManager : MonoBehaviour {
        [SerializeField] private Sprite[] player00Idle;
        [SerializeField] private Sprite[] player00Left;
        [SerializeField] private Sprite[] player00Right;

        public Sprite[] GetPlayerAnim(int ord, int dir) {
            switch (ord) {
                default:
                    switch (dir) {
                        case 0:
                            return player00Idle;
                        case 1:
                            return player00Left;
                        case 2:
                            return player00Right;
                    }
                    break;
            }
            return null;
        }

        private int _playerNum;
    }
}
