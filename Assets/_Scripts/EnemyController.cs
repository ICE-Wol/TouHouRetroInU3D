using System;
using UnityEngine;

namespace _Scripts {
    public class EnemyController : MonoBehaviour {
        [SerializeField] private Sprite[] _animFairy;
        private SpriteRenderer _spriteRenderer;
        private int _timer;
        private int _frameSpeed;
        private int _idlePointer;
        private int _movePointer;
        private Vector3 _prePosition;
        private Vector2 _direction;
        
        private void PlayAnim() {
            if (_timer % _frameSpeed == 0) {
                //get the direction
                float hor = _direction.x;
                if (Calc.Equal(hor, 0f)) {
                    //no horizontal movement.
                    if (_movePointer == 0) {
                        _idlePointer++;
                        if (_idlePointer == 4) _idlePointer = 0;
                        _spriteRenderer.sprite = _animFairy[_idlePointer];
                    }
                    //no horizontal movement but have side animation
                    else {
                        _movePointer -= Math.Sign(_movePointer);
                        _spriteRenderer.sprite = _animFairy[4 + Math.Abs(_movePointer)];
                        _spriteRenderer.flipX = (_movePointer < 0);
                    }
                }
                else {
                    _movePointer += (int)Mathf.Sign(hor);
                    if (Math.Abs(_movePointer) == 8) 
                        _movePointer -= 4 * Math.Sign(_movePointer);
                    _spriteRenderer.sprite = _animFairy[4 + Math.Abs(_movePointer)];
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
    }
}
