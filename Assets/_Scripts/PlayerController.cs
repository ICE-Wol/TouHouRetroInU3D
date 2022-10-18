using UnityEngine;

namespace _Scripts {
    public class PlayerController : MonoBehaviour {
        private Vector2 _direction;
        private float _slowMultiplier;

        public void SetSpeedX(bool isPositive) {
            _direction.x = isPositive ? 1 : -1;
        }
        
        public void SetSpeedY(bool isPositive) {
            _direction.y = isPositive ? 1 : -1;
        }

        public void SetSlow() {
            _slowMultiplier = 0.8f;
        }

        private void ResetState() {
            _direction = Vector2.zero;
            _slowMultiplier = 1f;
        }

        private void Movement() {
            
        }
        
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
