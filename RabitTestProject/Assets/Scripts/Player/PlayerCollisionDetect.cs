using UnityEngine;
namespace rabbit_game.player
{
    public class PlayerCollisionDetect : MonoBehaviour
    {
        [Header("Groun Control Variable")]
        [SerializeField] private LayerMask layermask;
        //private int layerMask = ~(1 << 7);
        //int layerMask = ~(1 << 1 | 1 << 2 | 1 << 3);
        [SerializeField] private Vector3 GroundSphereCenter = new Vector3(0, -0.7f, 0);
        [SerializeField] private float GroundSphereRadius = 0.6f;
        [SerializeField] private Vector3 HeadSphereCenter = new Vector3(0, 0.75f, 0);
        [SerializeField] private float HeadSphereRadius = 0.55f;
        [SerializeField] private Transform raycastPosition;
        private Color groundGizmosColor, headGizmozColor;

        [Header("Slope")]
        [SerializeField] private float slopeRayLength;
        [SerializeField] public RaycastHit slopeHit;
        [SerializeField] float slopeMaxAngle;
        [SerializeField]float slopeAngle;

        private void Update() {
        }

        public bool OverlapGroundChech() {
            int maxColliders = 10;
            Collider[] hitColliders = new Collider[maxColliders];
            int numColliders = Physics.OverlapSphereNonAlloc(transform.position + GroundSphereCenter, GroundSphereRadius, hitColliders, ~layermask);
            if (numColliders > 0) {
                groundGizmosColor = Color.yellow;
                return true;
            }
            else {
                groundGizmosColor = Color.red;
                return false;
            }
        }
        public bool OverlapHeadChech() {
            int maxColliders = 10;
            Collider[] hitColliders = new Collider[maxColliders];
            int numColliders = Physics.OverlapSphereNonAlloc(transform.position + HeadSphereCenter, HeadSphereRadius, hitColliders, ~layermask);
            if (numColliders > 0) {
                headGizmozColor = Color.red;
                return false;
            }
            else {
                headGizmozColor = Color.yellow;
                return true;
            }
        }
        public bool GroundCheck()//using raycast
        {
            RaycastHit hit;
            float distance = 0.2f;
            bool isGrounded;
            Vector3 dir = new Vector3(0, -1, 0);

            if (Physics.Raycast(raycastPosition.position, dir, out hit, distance)) {
                isGrounded = true;
            }
            else {
                isGrounded = false;
            }
            return isGrounded;
        }

        public bool SlopeRaycast()
        {
            Vector3 rayOrigin = raycastPosition.position;
            Vector3 rayDirection = Vector3.down;

            Debug.DrawRay(rayOrigin, rayDirection,slopeRayLength* Color.red);

            if(Physics.Raycast(rayOrigin, rayDirection, out slopeHit, slopeRayLength))
            {
                slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
                if(slopeAngle > slopeMaxAngle && slopeAngle != 0)
                    return true;
            }
            return false;
        }
        
        public bool OnWall()
        {
            float raycastDistance = 1f; // Raycast mesafesi

            Vector3[] directions = { transform.forward, -transform.forward, transform.right, -transform.right, transform.up, -transform.up };

            foreach (Vector3 direction in directions)
            {
                Debug.DrawRay(transform.position, direction, 2f * Color.red);

                if (Physics.Raycast(transform.position, direction, raycastDistance))
                {
                    return true;
                }
            }

            return false;
        }

        private void OnDrawGizmos() {
            //draw ground sphere
            Gizmos.color = groundGizmosColor;
            Gizmos.DrawSphere(transform.position + GroundSphereCenter, GroundSphereRadius);
            //draw head sphere
            Gizmos.color = headGizmozColor;
            Gizmos.DrawSphere(transform.position + HeadSphereCenter, HeadSphereRadius);
        }
    }
}
