using PhysicsBasedCharacterController;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rabbit_game.environment.platforms {
    public class PlatformTrigger : MonoBehaviour {

        private Vector3 lastEulerAngles;
        private Vector3 lastPosition;
        private void Awake() {
            lastPosition = transform.position;
            lastEulerAngles = transform.eulerAngles;
        }


        public Rigidbody playerrb;
        private void OnTriggerEnter(Collider other) {
            other.transform.parent.SetParent(transform);
        }
        private void OnTriggerExit(Collider other) {
            other.transform.parent.SetParent(null);
        }

        private void FixedUpdate() {
            UpdateBodies();
        }
        private void UpdateBodies() {
            Vector3 velocity = transform.position - lastPosition;
            Vector3 angularVelocity = transform.eulerAngles - lastEulerAngles;
            if (GetComponent<Rigidbody>().velocity.magnitude > 0) playerrb.velocity -= GetComponent<Rigidbody>().velocity/60;

            //playerrb.position += velocity;

            lastPosition = transform.position;
            lastEulerAngles = transform.eulerAngles;
        }
    }
}
