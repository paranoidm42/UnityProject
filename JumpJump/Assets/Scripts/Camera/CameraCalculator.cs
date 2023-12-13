using UnityEngine;

public class CameraCalculator : MonoBehaviour
{

    public static CameraCalculator instance;


    float cameraHeight;
    float cameraWeight;

    public float CameraHeight {get {return cameraHeight;}}
    public float CameraWeight {get {return cameraWeight;}}
    void Awake() 
    {
        if(instance == null)
            instance = this;
        else if(instance == this)
            Destroy(gameObject);

        cameraHeight = Camera.main.orthographicSize; 
        cameraWeight = cameraHeight * Camera.main.aspect;
    }

    
}
