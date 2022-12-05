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

        public bool CheckCollision(GameObject target, float r) {
            var d2 = (transform.position - target.transform.position).sqrMagnitude;
            return (radius * radius + r * r > d2);
        }

        public void CheckBound() {
            var pos = transform.position;
            if (Mathf.Abs(pos.x) > 6f || Mathf.Abs(pos.y) > 6f) {
                Release();
            }
        }

        public void Release() {
            _state = BulletStates.Inactivated;
            BulletManager.Manager.BulletPool.Release(this);
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
        public BulletStates GetState() => _state;

        public void SetState(BulletStates state) {
            _state = state;
            switch (state) {
                case BulletStates.Spawning:
                    //temp, using turing big instead of fog
                    transform.localScale = Vector3.zero;
                    _fogScale = 0f;
                    _fogAlpha = 0f;
                    break;
            }
        }

        private float _fogScale;
        private float _fogAlpha;
        
        private void FixedUpdate() {
            _timer++;
            CheckBound();
            switch (_state) {
                case BulletStates.Spawning:
                    _fogScale = Calc.Approach(_fogScale, 1f, 16f);
                    _fogAlpha = Calc.Approach(_fogAlpha, 1f, 16f);

                    transform.localScale = _fogScale * Vector3.one;
                    var c = spriteRenderer.color;
                    c.a = _fogAlpha;
                    spriteRenderer.color = c;

                    if (Calc.Equal(_fogScale, 1f, 0.01f))
                        SetState(BulletStates.Activated);
                    break;
            }
        }
    }
}
