using System;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace _Scripts {
    public class Particle : MonoBehaviour {
        [SerializeField] private SpriteRenderer spriteRenderer;
        private Sprite[] _anim;
        private int _frameSpeed;
        private int _spritePointer;
        private int _timer;
        private int _type;
<<<<<<< HEAD
        private float _direction;
        
        public void SetType(int character, int order) {
            _type = character * 10 + order;
=======

        public void SetType(int character, int order){}
        public void SetAnim(Sprite[] spr) {
>>>>>>> 7e0e1f1a39a1786db713427a7a0b35f9f196f88d
            _spritePointer = 0;
            _timer = 0;
            transform.localScale = Vector3.one;
            spriteRenderer.sprite = null;
            spriteRenderer.color = Calc.SetAlpha(spriteRenderer.color, 1f);
            _anim = ParticleManager.Manager.GetParticleAnim(character, order);
        }
        
        public void SetDirection(float direction) {
            _direction = direction;
        }
        
        void Start() {
            _timer = 0;
            _frameSpeed = 10;
            spriteRenderer = GetComponent<SpriteRenderer>();
            //SetAnim(ParticleManager.Manager.GetParticleAnim());
        }

        // Update is called once per frame
        void FixedUpdate() {
            transform.localScale += Time.fixedDeltaTime * 3f * Vector3.one;
<<<<<<< HEAD
            transform.position += Time.fixedDeltaTime * 3f * (Vector3)Calc.Degree2Direction(_direction);
            transform.rotation = Quaternion.Euler(0f,0f,_direction);
=======
            transform.position += Time.fixedDeltaTime * 3f * Vector3.up;
>>>>>>> 7e0e1f1a39a1786db713427a7a0b35f9f196f88d
            spriteRenderer.color = Calc.Fade(spriteRenderer.color, 5f);

            if (_anim == null) 
                ParticleManager.Manager.ParticlePool.Release(this);
            if (_timer % _frameSpeed == 0) {
                if (_spritePointer >= _anim.Length) {
                    ParticleManager.Manager.ParticlePool.Release(this);
                    //Note: this update will still be going shortly after the release function.
                    return;
                }
                else spriteRenderer.sprite = _anim[_spritePointer];
                _spritePointer++;
            }
            _timer++;
        }
    }
}
