using UnityEngine;

namespace _Scripts.Commands {
    public class PlayerBullet : MonoBehaviour {
        private float _speed;
        // Start is called before the first frame update
        void Start() {
            _speed = 12f;
        }

        // Update is called once per frame
        void FixedUpdate() {
            transform.position += _speed * Vector3.up * Time.fixedDeltaTime;
        }
    }
}
