using System.Collections;

using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    PlayerController playerController;
    PlayerInputManager controller;


    public AudioSource engineAS;
    public AudioSource tireAS;
    public AudioSource carHitAS;
    public AudioSource jumpCarAS;
    public AudioSource bostCarAS;

    float desiredEnginePitch = 0.5f;
    float tireScreechPit = 0.5f;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        controller = GetComponent<PlayerInputManager>();
    }

    void OnEnable()
    {
        PlayerCollision.onCameraShake += HitWallSFX;
        PlayerInputManager.bostCarHandler += BostCarSFX;

    }

    void OnDisable()
    {
        PlayerCollision.onCameraShake -= HitWallSFX;
        PlayerInputManager.bostCarHandler -= BostCarSFX;


    }
    private void Update()
    {
        JumpCarSFX();
        UpdateEngineSFX();
        UpdateTireSFX();
        
    }


    void UpdateEngineSFX()
    {
        float velocityMagnitude = playerController.rb.velocity.magnitude;

        float engineVolume = velocityMagnitude * 0.5f;

        engineVolume = Mathf.Clamp(engineVolume, 0.2f, 1.0f);

        engineAS.volume = Mathf.Lerp(engineAS.volume, engineVolume, Time.deltaTime * 10);

        desiredEnginePitch = velocityMagnitude * 0.2f;
        desiredEnginePitch = Mathf.Clamp(desiredEnginePitch, 0.5f, 2.0f);
        engineAS.pitch = Mathf.Lerp(engineAS.volume, desiredEnginePitch, Time.deltaTime * 1.5f);


    }
    void UpdateTireSFX()
    {
        if (playerController.lateralSpeed != 0 && !playerController.isJumping)
        {
              tireAS.volume = Mathf.Lerp(tireAS.volume, 1.0f, Time.deltaTime * 10f);
              tireScreechPit = Mathf.Lerp(tireScreechPit, 0.5f, Time.deltaTime * 10f);
        }
        else
        {
            tireAS.volume = 0f;
            tireScreechPit = 0;
        }
    }

    void JumpCarSFX()
    {
        float targetVolume = playerController.isJumping ? 5.0f : 0f;

        jumpCarAS.volume = Mathf.Lerp(jumpCarAS.volume, targetVolume, Time.deltaTime * 10f);
    }

    void BostCarSFX()
    {

        bostCarAS.volume = Mathf.Lerp(bostCarAS.volume, 1f, Time.deltaTime * 10f);

        if (!bostCarAS.isPlaying)
        {
            bostCarAS.Play();
        }

    }

 
    
    void HitWallSFX()
    {
        float velocityMagnitude = playerController.rb.velocity.magnitude;
        float volume = velocityMagnitude * 0.1f;
        carHitAS.pitch = Random.Range(0.95f, 1.05f);
        carHitAS.volume = volume;

        if(!carHitAS.isPlaying)
        {
            carHitAS.Play();
        }

    }

    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
    }
}

