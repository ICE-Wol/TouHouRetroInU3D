using UnityEngine;

namespace _Scripts.Title {
    public class LightSet : MonoBehaviour {
        public SpriteRenderer spriteRender;
        public float alpha;
        public float posX;
        public float yScale;
        public float mul;
        public int timer;
        void Start() {
            spriteRender = GetComponent<SpriteRenderer>();
            timer = Random.Range(0, 360);
            mul = Random.Range(-1f, 1f);
            posX = transform.position.x;
            yScale = transform.localScale.y;
            alpha = spriteRender.color.a;
        }

        // Update is called once per frame
        void Update() {
            timer++;
            float x = posX + Mathf.Sin(mul * timer / 50f* Mathf.Deg2Rad);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
            
            float y = yScale + Mathf.Cos(mul * timer / 30f * Mathf.Deg2Rad) * 0.1f;
            transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
            
            
        }
    }
}
