using System;
using System.Collections.Generic;
using _Scripts.Data;
using Unity.VisualScripting;
using UnityEngine;

namespace _Scripts {
    public class BulletController : MonoBehaviour {
        private List<Bullet> _bullets;
        private bool _isActivated;
        private int _ways;
        private float _initDeg;

        public void Activate(int ways, float initDeg) {
            _ways = ways;
            _initDeg = initDeg;
            GenerateBullets();
            _isActivated = true;
        }
        public void UpdateBullets() {
            int activeCount = 0;
            for(int i = 0;i < _bullets.Count;i++) {
                if (_bullets[i].GetState() != BulletStates.Inactivated) {
                    _bullets[i].transform.position
                        += (Vector3)(3f * Time.fixedDeltaTime * Calc.Deg2Dir(_initDeg + 360f * (i + 1) / _ways));
                    activeCount++;
                }
            }
            if(activeCount == 0) Destroy(this.gameObject);
        }

        public void GenerateBullets() {
            _bullets = new List<Bullet>();
            for (int i = 1; i <= _ways; i++) {
                var p = BulletManager.Manager.GetBullet();
                var d = new BulletData();
                p.SetData(d);
                p.transform.position = transform.position;
                _bullets.Add(p);
            }
        }

        public void CheckPlayerCollision(PlayerController player, float r) {
            foreach (var bullet in _bullets) {
                if (bullet.CheckCollision(player.gameObject, r)) {
                    bullet.Release();
                    player.GetHit();
                    break;
                }
            }
        }
        

        private void FixedUpdate() {
            if(_isActivated)
                UpdateBullets();
        }
    }
}
