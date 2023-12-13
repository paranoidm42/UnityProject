using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    float backgroundPosition;
    float distance = 10.74f;
    float swapPositionY;

    CameraCalculator cameraCalculator;
    void Start()
    {
        cameraCalculator = CameraCalculator.instance;
        backgroundPosition = transform.position.y;
        swapPositionY = transform.position.y;
      
        transform.localScale = new Vector2(cameraCalculator.CameraWeight - 0.2f, cameraCalculator.CameraHeight - 0.2f);
    }

    void Update()
    {
        if(backgroundPosition + distance < Camera.main.transform.position.y)
        {
            BackgroundRepositionUp();
        }
        else if(backgroundPosition - distance > Camera.main.transform.position.y)
        {
            BackgroundRepositionDown();
        }
    }

    void BackgroundRepositionUp()
    {
        backgroundPosition += (distance * 2);

        transform.position = new Vector2(0, backgroundPosition);
    }
    void BackgroundRepositionDown()
    {
        backgroundPosition -= (distance * 2);

        transform.position = new Vector2(0, backgroundPosition);
    }
}
