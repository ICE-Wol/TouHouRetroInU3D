using System;
using System.Collections.Generic;
using _Scripts.Data;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Scripts {
    public enum BulletStates {
        Inactivated,
        Spawning,
        Activated,
    }
    
    public class Bullet : MonoBehaviour {
        public SpriteRenderer spriteRenderer;
        public Transform parent;
        public int order;

        public int type;
        public int color;
        public Color uniqueColor;
        public Vector3 scale;
        public Vector3 direction;
        //private float _alpha; **already in color.
        public float speed;
        public float rotation;
        public float radius;
        public bool isGlowing;
        public bool isChecking;

        /*private int _movementMode;
        private int _generateMode;
        private int _releaseMode;*/

        private int _timer;
        private BulletStates _state;

        private void Awake() {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetData(BulletData data) {
            type = data.Type;
            color = data.Color;
            uniqueColor = data.UniqueColor;
            isGlowing = data.IsGlowing;
            scale = data.Scale;
            
            spriteRenderer.sprite
                = BulletManager.Manager.GetBulletSprite(type, color);
            spriteRenderer.material
                = BulletManager.Manager.GetBulletMaterial(isGlowing);
        }

        public bool CheckDistance(GameObject target, float r) {
            var d2 = (transform.position - target.transform.position).sqrMagnitude;
            return (radius * radius + r * r > d2);
        }

        /*private void Generate(int mode) {
            switch (mode) {
                default:
                    break;
                case 1:
                    if (_timer % 10 == 0) {
                        var p1 = BulletManager.Manager.BulletPool.Get();
                        p1.SetData(new BulletData());
                        p1.transform.position = this.transform.position;
                        p1.SetMovement(0, new float[] { _rotation + 30f, (1.5f + Mathf.Sin(Mathf.Deg2Rad*2f*_timer))/2f });

                        var p2 = BulletManager.Manager.BulletPool.Get();
                        p2.SetData(new BulletData());
                        p2.transform.position = this.transform.position;
                        p2.SetMovement(0, new float[] { _rotation - 30f, (1.5f + Mathf.Sin(Mathf.Deg2Rad*2f*_timer))/2f });
                    }

                    break;
            }
        }*/

        private void FixedUpdate() {
            _timer++;
        }
    }
}
