using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rabbit_game.environment.platforms
{
    public class RotatePlatform : MonoBehaviour
    {
        [field: SerializeField] private Vector3 rotateSpeed { get; set; }

        private Rigidbody _rigidbody;
        private Vector3 rotateAngle;

        private void Start() {
            _rigidbody = GetComponent<Rigidbody>();
            rotateAngle = gameObject.transform.eulerAngles;
        }
        private void FixedUpdate() {
            rotateAngle += rotateSpeed * Time.deltaTime;
            var rotation = Quaternion.Euler(rotateAngle);
            _rigidbody.MoveRotation(rotation);
        }
    }
}
