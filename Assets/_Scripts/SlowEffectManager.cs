using UnityEngine;

namespace _Scripts {
    public class SlowEffectManager : MonoBehaviour {
        public SpriteRenderer[] slowEffects;
        private float _tarAlpha;
        private float _curAlpha;
        private float _tarScaleLeft;
        private float _curScaleLeft;
        private float _tarScaleRight;
        private float _curScaleRight;

        private int _timer;
        void Start() {
            _tarAlpha = 0f;
            _curAlpha = 0f;
            _tarScaleLeft = 0f;
            _curScaleLeft = 0f;
            _tarScaleRight = 1.4f;
            _curScaleRight = 1.4f;
        }

        public void SetSlow() {
            _tarAlpha = 1f;
            _tarScaleLeft = 1f;
            _tarScaleRight = 1f;
        }

        public void SetNormal() {
            _tarAlpha = 0f;
            _tarScaleLeft = 0f;
            _tarScaleRight = 1.4f;
        }
        
        
        void Update() {
            _timer++;
            
            _curAlpha = Calc.Approach(_curAlpha, _tarAlpha, 32f);
            _curScaleLeft = Calc.Approach(_curScaleLeft, _tarScaleLeft, 64f);
            _curScaleRight = Calc.Approach(_curScaleRight, _tarScaleRight, 64f);
            
            slowEffects[0].transform.localScale = _curScaleLeft * Vector3.one;
            slowEffects[1].transform.localScale = _curScaleRight * Vector3.one;

            var color = slowEffects[1].color;
            color.a = _curAlpha;
            slowEffects[1].color = color;
            slowEffects[0].color = color;
            
            slowEffects[0].transform.rotation = Quaternion.Euler(0,0,_timer / 10f);
            slowEffects[1].transform.rotation = Quaternion.Euler(0,0,-_timer / 10f);

        }
    }
}
