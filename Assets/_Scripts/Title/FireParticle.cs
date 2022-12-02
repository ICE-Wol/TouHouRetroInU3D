using System.Security.Principal;
using UnityEngine;

namespace _Scripts.Title {
    public class FireParticle : MonoBehaviour {
        public float scale;
        public float speed;
        public float colorG;
        public float alpha;
        public float offset;
        public float height;

        public SpriteRenderer spriteRenderer;

        public void Refresh() {
            scale = Random.Range(0.2f, 0.5f);
            float y = Random.Range(-5f, -3f);
            float x = Random.Range(3.3f, 11f);
            float z = Random.Range(-10f, 5f);
            x *= Mathf.Sign(Random.Range(-1f, 1f));
            colorG = Random.Range(0, 60f/255f);
            offset = Random.Range(0, 360f);
            height = Random.Range(-0.25f, 0.25f);
            alpha = 0f;
            spriteRenderer.color = new Color(0f, colorG, 1f, 0f);
            transform.position = new Vector3(x, y, z);
        }

        // Update is called once per frame
        void Update() {
            transform.position += 0.5f * Time.deltaTime * Vector3.up
                                  + height * Mathf.Sin(Time.time + offset) * Time.deltaTime * Vector3.right;
            scale -= 0.0001f;
            transform.localScale = scale * Vector3.one;
            colorG += 0.0005f;
            alpha = Calc.Approach(alpha, Mathf.Abs(Mathf.Cos(Time.deltaTime / 10f - offset)), 64f);
            spriteRenderer.color = new Color(0f, colorG,1f, alpha);

            if (scale <= 0f) {
                Refresh();
                //ParticleManager.Manager.ParticlePool.Release(this);
            }
        }
    }
}
