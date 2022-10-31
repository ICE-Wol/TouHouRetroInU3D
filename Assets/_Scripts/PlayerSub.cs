using UnityEngine;

namespace _Scripts {
    public class PlayerSub : MonoBehaviour {
        [SerializeField] private SpriteRenderer shade;
        private int _timer;
        private int _type;
        void Start() {
            _timer = 0;
            _type = 1;
        }

        public void Fire(float direction) {
            var bullet = BulletManager.Manager.PlayerBulletPool.Get();
            bullet.SetPlayerBulletType(_type / 10, _type % 10);
            bullet.transform.position = transform.position;
            bullet.SetDirection(direction);
        }

        // Update is called once per frame
        void Update() {
            transform.rotation = Quaternion.Euler(0f,0f,_timer / 3f);
            shade.transform.rotation = Quaternion.Euler(0f,0f,-_timer / 3f);
            _timer++;
        }
    }
}
