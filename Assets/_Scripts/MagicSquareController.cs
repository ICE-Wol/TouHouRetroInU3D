using System;
using UnityEngine;

namespace _Scripts {
    public class MagicSquareController : MonoBehaviour {
        [SerializeField] private Transform parent;
        private int _timer;

        private void Start() {
            _timer = 0;
            if(parent != null)
                transform.position = parent.position;
        }

        void FixedUpdate() {
            _timer++;
            transform.rotation = Quaternion.Euler(
                45f * Mathf.Sin(0.15f * _timer * Mathf.Deg2Rad),
                -45f * Mathf.Sin(0.3f * _timer * Mathf.Deg2Rad),
                0.6f * (float)_timer);
            if(parent != null)
                transform.position = parent.position;
        }
    }
}
