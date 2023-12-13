using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AzeriPatates
{
    public class Cannon : MonoBehaviour
    {
        [SerializeField] Transform bullet;
        [SerializeField] Transform muzzle;
        [SerializeField] float Force;

        private void Start() {
            InvokeRepeating(nameof(Shoot),1,5);
        }
        private void Shoot() {
            Transform newBullet = Instantiate(bullet, muzzle.transform.position + (muzzle.up * 2), Quaternion.identity);
            newBullet.GetComponent<Rigidbody>().AddForce(-newBullet.right * Force);
        }
    }
}
