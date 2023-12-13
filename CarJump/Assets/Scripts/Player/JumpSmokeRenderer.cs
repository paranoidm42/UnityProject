using System.Collections;
using UnityEngine;

public class JumpSmokeRenderer : MonoBehaviour
{
    public ParticleSystem smokeRender;
    public PlayerInputManager controller;
    PlayerController playerController;


    void Start()
    {
        smokeRender = GetComponent<ParticleSystem>();
        controller = GetComponentInParent<PlayerInputManager>();
        playerController = GetComponentInParent<PlayerController>();
        smokeRender.Stop();

    }

    private void FixedUpdate()
    {
        if (controller.jumpCar && !playerController.isJumping) 
        {
            smokeRender.Play();
            StartCoroutine(JumpSmoke());
        }
    }

    IEnumerator JumpSmoke()
    {
        yield return new WaitForSeconds(0.2f);
        smokeRender.Stop();

    }
}
