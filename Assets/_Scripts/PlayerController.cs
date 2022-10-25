using System;
using _Scripts.Commands;
using UnityEngine;

namespace _Scripts {
    public class PlayerController : MonoBehaviour {
        [SerializeField] private SlowEffectManager effSlow;
        [SerializeField] private PlayerBullet playerBullet;
        private SpriteRenderer _spriteRenderer;
        private Vector2 _direction;
        private float _slowMultiplier;
        private float _slowRate;
        private float _moveSpeed;
        private int _frameSpeed;
        private int _idlePointer;
        private int _movePointer;

        private int _timer;
        

        public void SetSpeedX(bool isPositive) => _direction.x = isPositive ? 1 : -1;

        public void SetSpeedY(bool isPositive) => _direction.y = isPositive ? 1 : -1;

        public void SetSlow() {
            _slowMultiplier = _slowRate;
            effSlow.SetSlow();
        }

        private void ResetSpeed() => _direction = Vector2.zero;

        private void ResetSlow() => _slowMultiplier = 1f;

        
        private Sprite[] _animPlayerIdle;
        private Sprite[] _animPlayerLeft;
        private Sprite[] _animPlayerRight;
        private void GetAnim() {
            _animPlayerIdle  = GameManager.Manager.GetPlayerAnim(0, 0);
            _animPlayerLeft  = GameManager.Manager.GetPlayerAnim(0, 1);
            _animPlayerRight = GameManager.Manager.GetPlayerAnim(0, 2);
        }

        private void PlayAnim() {
            if (_timer % _frameSpeed == 0) {
                //get the direction
                int hor = (int)_direction.x;
                if (hor == 0) {
                    //Only when move pointer returned to zero can idle animation being played.
                    if (_movePointer == 0) {
                        _idlePointer++;
                        if (_idlePointer == 8) _idlePointer = 0;
                        _spriteRenderer.sprite = _animPlayerIdle[_idlePointer];
                    }
                    //If move pointer is not zero then make it naturally back to zero.
                    else {
                        _movePointer -= Math.Sign(_movePointer);
                        _spriteRenderer.sprite = _movePointer >= 0 ? 
                            _animPlayerRight[_movePointer] : _animPlayerLeft[-_movePointer];
                    }
                }
                else {
                    _movePointer += hor;
                    if (_movePointer == 8) _movePointer = 4;
                    if (_movePointer == -8) _movePointer = -4;
                    
                    _spriteRenderer.sprite = _movePointer >= 0 ? 
                        _animPlayerRight[_movePointer] : _animPlayerLeft[-_movePointer];
                }
            }
        }

        private void ResetState() {
            _direction = Vector2.zero;
            _slowMultiplier = 1f;
            effSlow.SetNormal();
        }

        /// <summary>
        /// Cooperate with the command manager.
        /// </summary>
        private void Movement() {
            transform.position += _moveSpeed * _slowMultiplier * Time.fixedDeltaTime
                                  * (Vector3)_direction.normalized;
        }

        public void Fire() {
            if (_timer % 2 == 0) {
                var bullet = BulletManager.Manager.PlayerBulletPool.Get();
                bullet.transform.position = transform.position + 0.15f * Vector3.left;
                bullet.transform.rotation = Quaternion.Euler(0, 0, 90f);

                bullet = BulletManager.Manager.PlayerBulletPool.Get();
                bullet.transform.position = transform.position - 0.15f * Vector3.left;
                bullet.transform.rotation = Quaternion.Euler(0, 0, 90f);
            }
        }
        
        void Start() {
            _moveSpeed = 5f;
            _frameSpeed = 4;
            _slowMultiplier = 1f;
            _slowRate = 0.6f;
            _timer = 0;
            _idlePointer = 0;
            _movePointer = 0;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            GetAnim();
        }
        
        void FixedUpdate() {
            _timer++;
            Movement();
            PlayAnim();
            //the anims are relied on states(such as directions)
            ResetState();
        }
    }
}
