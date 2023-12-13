using System.Collections;
using UnityEngine;
using System;

namespace rabbit_game.environment.platforms
{
    public class MovablePlatform : MonoBehaviour
    {
        [field: SerializeField] private float _moveTime { get; set; } = 3.0f;
        [field: SerializeField] private Vector3 MovePosition { get; set; }


        private Rigidbody _rigidbody;
        private Vector3 _startPosition;
        private Vector3 _targetPosition;

        public void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _startPosition = transform.position;
            _targetPosition = _startPosition + MovePosition;
        }

        private void FixedUpdate()
        {
            MovePlatform();
        }

        private void MovePlatform() {
            var t = Utils.EaseInOut(Mathf.PingPong(Time.time, _moveTime), _moveTime);
            var p = Vector3.Lerp(_startPosition, _targetPosition, t);
            _rigidbody.MovePosition(p);
        }

        //public Rigidbody rb;
        //private void OnCollisionStay(Collision collision) {
        //    if (collision.gameObject.tag == "Player") {
        //        rb = collision.gameObject.GetComponent<Rigidbody>();
        //        rb.velocity = new Vector3(_rigidbody.velocity.x, rb.velocity.y, _rigidbody.velocity.z);
        //    }
        //}
    }
}
