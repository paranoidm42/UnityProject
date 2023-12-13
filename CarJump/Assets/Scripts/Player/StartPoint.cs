
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    [SerializeField]Transform player;
    PlayerController playerController;
    bool startCar;


    void OnEnable()
    {
        PlayerInputManager.bostCarHandler += StartCar;
        PlayerCollision.onGameOver += StopCar;
        GameOverMenu.onRetry += StartCar;
        GameOverMenu.onRetry += MovePlayer;

    }
    void OnDisable()
    {
        PlayerInputManager.bostCarHandler -= StartCar;
        PlayerCollision.onGameOver -= StopCar;
        GameOverMenu.onRetry -= StartCar;
        GameOverMenu.onRetry -= MovePlayer;
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = player.GetComponent<PlayerController>();
       

        MovePlayer();

        StopCar();
    }

    void MovePlayer()
    {
        startCar = true;
        player.localPosition = transform.position;
        player.rotation = transform.rotation;
        playerController.rotationAngle = transform.localRotation.eulerAngles.z;
    }

    void StopCar() //Bunun yeri değişebilir çünkü araba ile alakalı.
    {
        playerController.GetComponent<Rigidbody2D>().drag = 10000;
        playerController.everythingİsOkey = false;
    }

    void StartCar()
    {
        if(startCar) 
        {
            playerController.GetComponent<Rigidbody2D>().drag = 0;
            playerController.everythingİsOkey = true;
            startCar = false;
        }
    }
}
