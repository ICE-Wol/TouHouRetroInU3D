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
        private int _damage;
        private int _timer;
        
        /// <summary>
        /// Set the type of the player bullet by character * 10 + order;
        /// </summary>
        /// <param name="character"></param>
        /// <param name="order">0 refer to the main fire.</param>
        public void SetPlayerBulletType(int character, int order) {
            _type = character * 10 + order;
            spritesRenderer.sprite = BulletManager.Manager.GetPlayerBulletSprite(character, order);
            
            if (_type % 10 == 0) _speed = 30f;
            else _speed = 4f;
            
            _radius = 0.06f;
            _damage = 1;
        }

        public void SetDirection(float direction) {
            _direction = direction;
        }

        void Movement() {
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
                    transform.position += _speed * Time.fixedDeltaTime * (Vector3)Calc.Degree2Direction(_direction);
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
            //single bullet
            if (_nearestEnemy != null && _radius + _nearestEnemy.Radius >= minDis) {
                _nearestEnemy.TakeDamage(_damage);
                
                var p = ParticleManager.Manager.ParticlePool.Get();
                p.SetAnim(ParticleManager.Manager.GetParticleAnim(1));
                p.transform.position = transform.position;
                
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
