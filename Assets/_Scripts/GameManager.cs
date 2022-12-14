using System;
using _Scripts.Data;
using UnityEngine;

namespace _Scripts {
    public class GameManager : MonoBehaviour {
        public static GameManager Manager;
        public PlayerData PlayerData;

        private void Awake() {
            if (Manager == null) {
                Manager = this;
            }
            else {
                Destroy(this.gameObject);
            }
            
            PlayerData = new PlayerData();
        }
        
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
    }
}
