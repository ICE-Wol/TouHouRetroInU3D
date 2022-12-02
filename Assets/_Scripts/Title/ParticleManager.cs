using UnityEngine;
using UnityEngine.Pool;

namespace _Scripts.Title {
    class ParticleManager : MonoBehaviour {
        [SerializeField] private FireParticle particle;
        public ObjectPool<FireParticle> ParticlePool;
        public static ParticleManager Manager;

        private void Awake() {
            if (!Manager) {
                Manager = this;
            }
            else {
                Destroy(this.gameObject);
            }
        }

        private void Start() {
            ParticlePool = new ObjectPool<FireParticle>(() => {
                return Instantiate(particle);
            }, bullet => {
                bullet.gameObject.SetActive(true);
            }, bullet => {
                bullet.gameObject.SetActive(false);
            }, bullet => {
                Destroy(bullet.gameObject);
            }, false, 50, 500);
            
            for(int i = 1;i <= 500;i++) {
                var p = ParticlePool.Get();
                p.Refresh();
            }
    }
    }
}
