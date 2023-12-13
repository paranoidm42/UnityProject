using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzeriPatates {
    public class CannonBullet : MonoBehaviour {
        [SerializeField] private float destroyTime;
        private void Start() {
            Destroy(gameObject, destroyTime);
        }

    }
}
