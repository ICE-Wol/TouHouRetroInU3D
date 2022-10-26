using System;
using UnityEngine;

namespace _Scripts {
    public class EnemyController : MonoBehaviour {
        [SerializeField] private Sprite[] animFairy;
        private SpriteRenderer _spriteRenderer;
        private int _timer;
        private int _frameSpeed;
        private int _idlePointer;
        private int _movePointer;
        private Vector3 _prePosition;
        private Vector2 _direction;

        public int Health { private set; get; }
        public int MaxHealth { private set; get; }
        public float Radius { private set; get; }

        public void TakeDamage(int dam) {
            Health -= dam;
            if(Health <= 0) Break();
        }

        private void Break() {
            EnemyManager.Manager.enemyList.Remove(this);
            //TODO: create some particles here.
        }
        
        private void PlayAnim() {
            if (_timer % _frameSpeed == 0) {
                //get the direction
                float hor = _direction.x;
                if (Calc.Equal(hor, 0f)) {
                    //no horizontal movement.
                    if (_movePointer == 0) {
                        _idlePointer++;
                        if (_idlePointer == 4) _idlePointer = 0;
                        _spriteRenderer.sprite = animFairy[_idlePointer];
                    }
                    //no horizontal movement but have side animation
                    else {
                        _movePointer -= Math.Sign(_movePointer);
                        _spriteRenderer.sprite = animFairy[4 + Math.Abs(_movePointer)];
                        _spriteRenderer.flipX = (_movePointer < 0);
                    }
                }
                else {
                    _movePointer += (int)Mathf.Sign(hor);
                    if (Math.Abs(_movePointer) == 8) 
                        _movePointer -= 4 * Math.Sign(_movePointer);
                    _spriteRenderer.sprite = animFairy[4 + Math.Abs(_movePointer)];
                    _spriteRenderer.flipX = (_movePointer < 0);
                }
            }
        }
        // Start is called before the first frame update
        void Start() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _timer = 0;
            _frameSpeed = 4;
            _movePointer = 0;
            Health = 500;
            MaxHealth = 500;
            Radius = 0.1f;
            _prePosition = transform.position;
        }

        // Update is called once per frame
        void FixedUpdate() {
            _timer++;
            transform.position = Vector3.zero + Vector3.right * Mathf.Sin(Mathf.Deg2Rad * _timer / 5f);
            _direction = (transform.position - _prePosition).normalized;
            PlayAnim();
            _prePosition = transform.position;
        }

        private void OnDrawGizmos() {
            Gizmos.DrawWireSphere(transform.position, Radius);
        }
    }
}
