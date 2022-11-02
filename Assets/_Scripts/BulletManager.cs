using UnityEngine;
using UnityEngine.Pool;

namespace _Scripts {
    public class BulletManager : MonoBehaviour {
        [SerializeField] private PlayerBullet playerBullet;
        [SerializeField] private Bullet enemyBullet;
        [SerializeField] private Sprite[] playerBulletSprites;
        [SerializeField] private Sprite[] bulletSprites;
        [SerializeField] private Material[] glowMaterial;
        public ObjectPool<PlayerBullet> PlayerBulletPool;
        public ObjectPool<Bullet> BulletPool;
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
            
            BulletPool = new ObjectPool<Bullet>(() => {
                return Instantiate(enemyBullet);
            }, bullet => {
                bullet.gameObject.SetActive(true);
            }, bullet => {
                bullet.gameObject.SetActive(false);
            }, bullet => {
                Destroy(bullet.gameObject);
            }, false, 300, 5000);
        }

        public Sprite GetPlayerBulletSprite(int character, int type) {
            return playerBulletSprites[type];
        }

        public Sprite GetBulletSprite(int type, int color) {
            return bulletSprites[type * 16 + color];
        }

        public Material GetBulletMaterial(bool isGlowing) {
            return isGlowing ? glowMaterial[1] : glowMaterial[0];
        }
    }
}
