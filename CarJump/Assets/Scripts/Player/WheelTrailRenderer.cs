
using UnityEngine;

public class WheelTrailRenderer : MonoBehaviour
{
    TrailRenderer trailRenderer;
    PlayerController playerController;
    public PlayerInputManager controller;

    private void OnEnable() {
        PlayerCollision.onGameOver += ResetEffect;
    }

    private void OnDisable() {
        PlayerCollision.onGameOver -= ResetEffect;

    }

    void Start()
    {
        controller = GetComponentInParent<PlayerInputManager>();
        trailRenderer = GetComponent<TrailRenderer>();
        playerController = GetComponentInParent<PlayerController>();
        trailRenderer.emitting = false;
    }

    void Update()
    {

        bool isMoving = controller.move != Vector2.zero && playerController.isRunCar;
        trailRenderer.emitting = isMoving;
    }

    void ResetEffect()
    {
        trailRenderer.Clear();
    }
}
