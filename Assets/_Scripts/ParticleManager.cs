using UnityEngine;
using UnityEngine.Pool;

namespace _Scripts {
    public class ParticleManager : MonoBehaviour {
        [SerializeField] private Sprite[] player00Shoot00;
        [SerializeField] private Sprite[] player00Shoot01;
        
        [SerializeField] private Particle particle;
        public ObjectPool<Particle> ParticlePool;
        public static ParticleManager Manager;

        private void Awake() {
            if (!Manager) {
                Manager = this;
            }
            else {
                Destroy(this.gameObject);
            }
        }

        public Sprite[] GetParticleAnim(int character, int ord) {
            switch (ord) {
                 default:
                     return player00Shoot00;
                 case 1:
                     return player00Shoot01;
            }
        }
        
        private void Start() {
            ParticlePool = new ObjectPool<Particle>(() => {
                return Instantiate(particle);
            }, bullet => {
                bullet.gameObject.SetActive(true);
            }, bullet => {
                bullet.gameObject.SetActive(false);
            }, bullet => {
                Destroy(bullet.gameObject);
            }, false, 50, 100);
        }
    }
}
