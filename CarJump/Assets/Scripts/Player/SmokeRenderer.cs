
using UnityEngine;

public class SmokeRenderer : MonoBehaviour
{
    
    public ParticleSystem smokeRender;
    public Rigidbody2D parentRb;
    
    private void OnEnable() {
        PlayerCollision.onGameOver += ResetEffect;
    }

    private void OnDisable() {
        PlayerCollision.onGameOver -= ResetEffect;

    }
    void Start()
    {
        parentRb = gameObject.GetComponentInParent<Rigidbody2D>();
        smokeRender = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (parentRb.velocity != Vector2.zero)
            smokeRender.Play();
        else
            smokeRender.Stop();
    }

    void ResetEffect()
    {
        smokeRender.Clear();
    }
}
