using System;
using UnityEngine;

namespace _Scripts {
    public class PlayerController : MonoBehaviour {
        private Vector2 _direction;
        private float _slowMultiplier;
        private float _slowRate;
        private float _speed;

        public void SetSpeedX(bool isPositive) => _direction.x = isPositive ? 1 : -1;

        public void SetSpeedY(bool isPositive) => _direction.y = isPositive ? 1 : -1;

        public void SetSlow() => _slowMultiplier = _slowRate;

        private void ResetSpeed() => _direction = Vector2.zero;

        private void ResetSlow() => _slowMultiplier = 1f;

        private void ResetState() {
            _direction = Vector2.zero;
            _slowMultiplier = 1f;
        }

        private void Movement() {
            transform.position += _speed * _slowMultiplier * Time.fixedDeltaTime
                                  * (Vector3)_direction.normalized;
            if(_slowMultiplier <= 0.9f) Debug.Log(_direction.normalized);
            ResetState();
        }
        
        // Start is called before the first frame update
        void Start() {
            _speed = 6f;
            _slowMultiplier = 1f;
            _slowRate = 0.6f;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Movement();
        }
    }
}
