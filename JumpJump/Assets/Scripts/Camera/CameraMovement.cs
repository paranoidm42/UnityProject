using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3 locationUpdate;
    float smoothSpeed = 0.125f;
    bool cameraMovementStatus;

    private void Start() 
    {
        cameraMovementStatus = true;    
    }

    private void LateUpdate() 
    {
        if(cameraMovementStatus)
        {
            MovementCamera();
        }
    }
    
    private void MovementCamera() 
    {
        
        locationUpdate = new Vector3(0, Mathf.Clamp(PlayerController.Instance.playerPositionY, 5f, Mathf.Infinity) , -10);

        transform.position = Vector3.Lerp(transform.position,locationUpdate, smoothSpeed);
    }
}
