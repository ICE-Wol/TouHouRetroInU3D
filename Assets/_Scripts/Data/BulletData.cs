using UnityEngine;
namespace _Scripts.Data {
    public class BulletData {
        public int Type;
        public int Color;
        public Color UniqueColor;
        public Vector3 Scale;
        public float Radius;
        public bool IsGlowing;
        
        public int MovementMode;
        public int GenerateMode;
        public int ReleaseMode;

        public BulletData() {
            Type = 3;
            Color = 11;
            Radius = 0.1f;
            UniqueColor = UnityEngine.Color.white;
            Scale = Vector3.one;
            IsGlowing = false;
            
            MovementMode = 0;
            GenerateMode = 0;
            ReleaseMode = 0;
        }
    }

}