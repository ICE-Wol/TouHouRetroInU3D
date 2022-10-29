using UnityEngine;

namespace _Scripts.Data {
    public class PlayerData {
        public int Type;
        public int Life;
        public int LifePiece;
        public int Bomb;
        public int BombPiece;
        public float Power;
        public int Graze;
        public int MaxPoint;
        public int Score;
        public int MaxScore;

        public PlayerData() {
            Type = 0;
            Life = 2;
            LifePiece = 0;
            Bomb = 3;
            BombPiece = 0;
            Power = 1f;
            Graze = 0;
            MaxPoint = 1000;
            Score = 0;
            MaxScore = 0;
        }
    }
}
