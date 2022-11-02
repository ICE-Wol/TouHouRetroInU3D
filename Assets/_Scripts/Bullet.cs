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
        private SpriteRenderer _spriteRenderer;
        private Transform _parent;
        private int _order;
        private List<Bullet> _childList;

        private int _type;
        private int _color;
        private Color _uniqueColor;
        private Vector3 _scale;
        private Vector3 _direction;
        //private float _alpha; **already in color.
        private float _speed;
        private float _rotation;
        private float _radius;
        private bool _isGlowing;
        private bool _isChecking;

        private int _movementMode;
        private int _generateMode;
        private int _releaseMode;

        private int _timer;
        private BulletStates _state;

        private void Awake() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetData(BulletData data) {
            _type = data.Type;
            _color = data.Color;
            _uniqueColor = data.UniqueColor;
            _isGlowing = data.IsGlowing;
            _scale = data.Scale;
            
            _spriteRenderer.sprite
                = BulletManager.Manager.GetBulletSprite(_type, _color);
            _spriteRenderer.material
                = BulletManager.Manager.GetBulletMaterial(_isGlowing);

            _movementMode = data.MovementMode;
            _generateMode = data.GenerateMode;
            _releaseMode = data.ReleaseMode;
            
        }

        public void Refresh() {
            _spriteRenderer.color = _uniqueColor;
            transform.localScale = _scale;
            transform.rotation = Quaternion.Euler(0,0,_rotation);
        }

        public void SetMovement(int mode, float[] args) {
            switch (mode) {
                default:
                    _direction = Calc.Degree2Direction(args[0]);
                    _speed = args[1];
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
        }

        private void FixedUpdate() {
            Refresh();
            Movement(_movementMode);
            Generate(_generateMode);
            _timer++;
        }
    }
}
