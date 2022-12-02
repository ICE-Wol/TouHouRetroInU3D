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

        /*public void Refresh() {
            spriteRenderer.color = uniqueColor;
            transform.localScale = scale;
            transform.rotation = Quaternion.Euler(0,0,rotation);
        }

        public void SetMovement(int mode, float[] args) {
            switch (mode) {
                default:
                    direction = Calc.Degree2Direction(args[0]);
                    speed = args[1];
                    break;
            }
        }

        private void Movement(int mode) {
            switch (mode) {
                default:
                    _rotation = Vector2.SignedAngle(Vector2.right, _direction) - 90f;
                    transform.position += _speed * Time.deltaTime * _direction;
                    break;
            }
            
        }

        private void Generate(int mode) {
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
        }

        private void Release(int mode) {
            switch (mode) {
                default:
                    break;
            }
        }*/

        private void FixedUpdate() {
            //Refresh();
            //Movement(_movementMode);
            //Generate(_generateMode);
            _timer++;
        }
    }
}
