using UnityEngine;

public class LavaController : MonoBehaviour
{

    float lavaSpeed = 1f;
    float lavaYPostion;
    float acceleration = 0.05f;
    float currentSpeed;
    bool onGameOver;

    bool onPlayGame;

    PlayerController playerController;

    Rigidbody2D rb;

    private void OnEnable() 
    {
        EventManager.StartListening<bool>("GameOver", (x) => { onGameOver = x; });
        EventManager.StartListening<bool>("PlayButton", (y) => { onPlayGame = y; });
    }
    private void OnDisable() 
    {
        EventManager.StopListening<bool>("GameOver", (x) => { onGameOver = x; });
        EventManager.StopListening<bool>("PlayButton", (y) => { onPlayGame = y; });
    }
    private void Start() 
    {
        playerController = PlayerController.Instance;
        rb = gameObject.GetComponent<Rigidbody2D>();
        onPlayGame = false;
        onGameOver = false;
        lavaSpeed = 1;
        currentSpeed += lavaSpeed;
        

    }

    void Update()
    {   
        int levelİnt = Mathf.FloorToInt(playerController.playerPositionY / 70) + 1;
        if(!onGameOver && onPlayGame)
        {
            currentSpeed = Mathf.Clamp(currentSpeed, 0f, levelİnt - 0.05f);
            lavaYPostion = currentSpeed;
            
            rb.velocity = Vector2.up * lavaYPostion;
            currentSpeed += acceleration * Time.smoothDeltaTime;
        }
    }
    


}
