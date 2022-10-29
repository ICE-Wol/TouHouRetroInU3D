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

        public void SetAnim(Sprite[] spr) {
            _spritePointer = 0;
            _timer = 0;
            transform.localScale = Vector3.one;
            spriteRenderer.sprite = null;
            spriteRenderer.color = Calc.SetAlpha(spriteRenderer.color, 1f);
            this._anim = spr;
        }
        
        void Start() {
            _timer = 0;
            _frameSpeed = 10;
            spriteRenderer = GetComponent<SpriteRenderer>();
            SetAnim(ParticleManager.Manager.GetParticleAnim(1));
        }

        // Update is called once per frame
        void FixedUpdate() {
            transform.localScale += Time.fixedDeltaTime * 3f * Vector3.one;
            transform.position += Time.fixedDeltaTime * 3f * Vector3.up;
            spriteRenderer.color = Calc.Fade(spriteRenderer.color, 4f);

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
