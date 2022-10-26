using System.Runtime.CompilerServices;
using UnityEngine;

namespace _Scripts {
    public class Particle : MonoBehaviour {
        private Sprite[] _anim;
        private int _frameSpeed;
        private int _spritePointer;
        private int _timer;

        public void SetAnim(Sprite[] anim) {
            _anim = anim;
        }
        
        void Start() {
            _timer = 0;
            _frameSpeed = 4;
        }

        // Update is called once per frame
        void Update() {
            _timer++;
            if (_timer % _frameSpeed == 0) {
                _spritePointer++;
                if (_spritePointer >= _anim.Length) {
                    //TODO: let the pool release.
                }
            }
        }
    }
}
