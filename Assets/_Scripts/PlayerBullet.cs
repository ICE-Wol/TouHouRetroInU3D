using System;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts {
    public class PlayerBullet : MonoBehaviour {
        [SerializeField] private SpriteRenderer spritesRenderer;
        private EnemyController _nearestEnemy;
        private int _type;
        private float _speed;
        private float _radius;
        private float _direction;
        private float _targetDirection;
        private int _damage;
        private int _timer;
        
        /// <summary>
        /// Set the type of the player bullet by character * 10 + order;
        /// </summary>
        /// <param name="character"></param>
        /// <param name="order">0 refer to the main fire.</param>
        public void SetPlayerBulletType(int character, int order) {
            _timer = 0;
            _type = character * 10 + order;
            spritesRenderer.sprite = BulletManager.Manager.GetPlayerBulletSprite(character, order);

            if (_type % 10 == 0) {
                _speed = 30f;
                _direction = 90f;
                transform.rotation = Quaternion.Euler(0f, 0f, _direction);
            }
            else _speed = 4f;
            
            _radius = 0.06f;
            _damage = 0;
            _type = character * 10 + order;
            spritesRenderer.sprite = BulletManager.Manager.GetPlayerBulletSprite(character, order);
        }

        public void SetDirection(float direction) {
            _direction = direction;
        }

        void Movement() {
            switch(_type) {
                case 1:
                    //if no signed the bullet would like wave
                    //homing bullet
                    if (_timer < 300 && _timer > 0 && _timer % 10 == 0) 
                        _targetDirection = Vector2.SignedAngle(Vector2.right,
                            _nearestEnemy.transform.position - transform.position);
                    if (_timer > 0) {
                        //TODO: maybe have better ways.
                        var tmp = _targetDirection;
                        if (_targetDirection - _direction > 180f) {
                            _targetDirection -= 360f;
                        }

                        if (_direction - _targetDirection > 180f) {
                            _targetDirection += 360f;
                        }

                        //_direction = Calc.Approach(_direction, _targetDirection, 8f);
                        if (_direction >= _targetDirection) _direction -= 5f;
                        else _direction += 5f;
                    }
                    break;
            }
            
            transform.position += _speed * Time.fixedDeltaTime * (Vector3)Calc.Deg2Dir(_direction);
            transform.rotation = Quaternion.Euler(0f, 0f, _direction);
            if(_timer > 1500f)
                BulletManager.Manager.PlayerBulletPool.Release(this);
        }

        void MakeParticle(int character, int order) {
            var p = ParticleManager.Manager.ParticlePool.Get();
            p.SetType(character, order);
            p.SetDirection(_direction);
            p.transform.position = transform.position;
            
            switch(_type)
            {
                default:
                    transform.position += _speed * Time.fixedDeltaTime * Vector3.up;
                    transform.rotation = Quaternion.Euler(0f,0f,90f);
                    if (transform.position.y >= 10f) 
                        BulletManager.Manager.PlayerBulletPool.Release(this);
                    break;
                
                case 1:
                    var tar = Vector2.SignedAngle(Vector2.right,_nearestEnemy.transform.position - transform.position);
                    if(_timer < 300f) _direction = Calc.Approach(_direction, tar, 4f);
                    transform.position += _speed * Time.fixedDeltaTime * (Vector3)Calc.Deg2Dir(_direction);
                    transform.rotation = Quaternion.Euler(0f,0f,_direction);
                    if(_timer > 3000f)
                        BulletManager.Manager.PlayerBulletPool.Release(this);
                    break;
                    
                
            }
        }

        void CheckHit() {
            float minDis = 1000f;
            foreach (var enemy in EnemyManager.Manager.enemyList) {
                var dis = ((Vector2)(enemy.transform.position - transform.position)).magnitude;
                if (dis < minDis) {
                    minDis = dis;
                    _nearestEnemy = enemy;
                }
            }
            //single hit bullet
            if (_nearestEnemy != null && _radius + _nearestEnemy.Radius >= minDis) {
                _nearestEnemy.TakeDamage(_damage);
                MakeParticle(_type / 10, _type % 10);
                BulletManager.Manager.PlayerBulletPool.Release(this);
            }
        }

        // Update is called once per frame
        void FixedUpdate() {
            CheckHit();
            Movement();
            _timer++;
        }
    }
}
