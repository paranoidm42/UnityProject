using UnityEngine;

public class DynamicPlatformMovement : MonoBehaviour
{

    float speed;
    float swapCameraWeight;
    float objectHalfWeight;
    float startPositionX;
    float pingPongX;
    
    void Start()
    {
        speed = 0.7f;
        swapCameraWeight = CameraCalculator.instance.CameraWeight;
        objectHalfWeight = transform.localScale.x / 2;
        startPositionX = transform.position.x;
    }

    void Update()
    {
        PlatformMove();
    }
    void PlatformMove()
    {
        pingPongX = Mathf.PingPong(speed * Time.time, swapCameraWeight - objectHalfWeight);
        if(startPositionX < 0)
            pingPongX *= -1;

        Vector2 platformPosition = new Vector2(pingPongX, transform.position.y);
        transform.position = platformPosition;
    }
    
}
