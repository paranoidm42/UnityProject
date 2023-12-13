using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace rabbit_game.environment
{
    public class Wind : MonoBehaviour {
        [SerializeField] private Vector3 center;
        [SerializeField] private Vector3 size;
        private float maxDistance;
        [SerializeField] private int maxCollisionCount;

        [SerializeField] float windForce;

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
            Gizmos.DrawWireCube(center, size);
        }

        private void FixedUpdate() {
            Vector3 castDirection = transform.forward;
            RaycastHit[] hits = new RaycastHit[maxCollisionCount];
            Physics.BoxCastNonAlloc(transform.position + center, size / 2f, castDirection, hits, transform.rotation, maxDistance);
            //RaycastHit[] hits = Physics.BoxCastAll(transform.position + center, size / 2f, castDirection, transform.rotation, maxDistance);
            if (hits.Length < 1) return;
            for (int i = 0; i < hits.Length; i++) {
                if (hits[i].collider == null) return;
                    GameObject hitObject = hits[i].collider.gameObject;
                if (hitObject.TryGetComponent(out Rigidbody rb)) {
                    rb.AddForce(transform.forward * windForce, ForceMode.Impulse);
                }
            }
        }


    }
}
