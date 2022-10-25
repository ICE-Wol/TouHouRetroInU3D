using UnityEngine;
using UnityEngine.Pool;

namespace _Scripts {
    public class BulletManager : MonoBehaviour {
        [SerializeField] private PlayerBullet playerBullet;
        public ObjectPool<PlayerBullet> PlayerBulletPool;
        public static BulletManager Manager;

        private void Awake() {
            if (!Manager) {
                Manager = this;
            }
            else {
                Destroy(this.gameObject);
            }
        }
        
        private void Start() {
            PlayerBulletPool = new ObjectPool<PlayerBullet>(() => {
                return Instantiate(playerBullet);
            }, bullet => {
                bullet.gameObject.SetActive(true);
            }, bullet => {
                bullet.gameObject.SetActive(false);
            }, bullet => {
                Destroy(bullet.gameObject);
            }, false, 50, 100);
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
