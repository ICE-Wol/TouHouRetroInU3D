using UnityEngine;

namespace _Scripts {
    public class PlayerBullet : MonoBehaviour {
        private float _speed;
        // Start is called before the first frame update
        void Start() {
            _speed = 30f;
        }

        // Update is called once per frame
        void FixedUpdate() {
            transform.position += _speed * Vector3.up * Time.fixedDeltaTime;
            if (transform.position.y >= 10f) BulletManager.Manager.PlayerBulletPool.Release(this);
        }
    }
}
