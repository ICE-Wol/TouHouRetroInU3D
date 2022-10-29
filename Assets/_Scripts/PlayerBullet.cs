using System;
using UnityEngine;

namespace _Scripts {
    public class PlayerBullet : MonoBehaviour {
        private float _speed;
        private float _radius;
        private int _damage;

        void CheckHit() {
            float minDis = 1000f;
            EnemyController nearestEnemy = null;
            foreach (var enemy in EnemyManager.Manager.enemyList) {
                var dis = ((Vector2)(enemy.transform.position - transform.position)).magnitude;
                if (dis < minDis) {
                    minDis = dis;
                    nearestEnemy = enemy;
                }
            }
            //single bullet
            if (nearestEnemy != null && _radius + nearestEnemy.Radius >= minDis) {
                nearestEnemy.TakeDamage(_damage);
                
                var p = ParticleManager.Manager.ParticlePool.Get();
                p.SetAnim(ParticleManager.Manager.GetParticleAnim(1));
                p.transform.position = transform.position;
                
                BulletManager.Manager.PlayerBulletPool.Release(this);
            }
        }
        
        void Start() {
            _speed = 30f;
            _radius = 0.06f;
            _damage = 1;
        }

        // Update is called once per frame
        void FixedUpdate() {
            CheckHit();
            transform.position += _speed * Vector3.up * Time.fixedDeltaTime;
            if (transform.position.y >= 10f) BulletManager.Manager.PlayerBulletPool.Release(this);
        }
    }
}
