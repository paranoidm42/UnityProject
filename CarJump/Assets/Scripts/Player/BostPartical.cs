using System.Collections;
using UnityEngine;

public class BostPartical : MonoBehaviour
{
    ParticleSystem pparticleSystem;
    void Start()
    {
        pparticleSystem = GetComponent<ParticleSystem>();
        pparticleSystem.Stop();


    }
    void OnEnable()
    {
        PlayerInputManager.bostCarHandler += BostCar;
    }
    void OnDisable()
    {
        PlayerInputManager.bostCarHandler -= BostCar;
    }
    
    void BostCar()
    {
        StartCoroutine(BostEffectTimer());
    }

    IEnumerator BostEffectTimer()
    {
        pparticleSystem.Play();
        yield return new WaitForSeconds(0.5f);
        pparticleSystem.Stop();
    }
}
