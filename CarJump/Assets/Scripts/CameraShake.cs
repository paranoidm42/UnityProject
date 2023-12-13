using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    CinemachineBasicMultiChannelPerlin perlin;
    public float newNoiseValue;
    public float shakeTime;

    private void OnEnable()
    {
        PlayerCollision.onCameraShake += ShakeCamera;
    }

    private void OnDisable()
    {
        PlayerCollision.onCameraShake -= ShakeCamera;
    }

    private void Start()
    {
        perlin = vcam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        perlin.m_FrequencyGain = 1f;

    }

    void ShakeCamera()
    {
        if(perlin != null) 
            StartCoroutine(FrequencyGain());
    }

    IEnumerator FrequencyGain()
    {
        perlin.m_FrequencyGain = newNoiseValue;
        yield return new WaitForSeconds(shakeTime);
        perlin.m_FrequencyGain = 1f;
    }
}
