using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private void Awake() 
    {
        if(Instance == null)
           Instance = this;
        else if(Instance == this)
            Destroy(gameObject);
    }

    public float movementSpeed;
    public float jumpHeight;
    public bool wallJumpCheck;

    public float playerPositionY;
    public bool onPlayGame;
    bool doubleJump;

    public Rigidbody2D rb;

    private void OnEnable() 
    {
        EventManager.StartListening<bool>("PlayButton", (y) => { onPlayGame = y; });
    }
    private void OnDisable() 
    {
        EventManager.StopListening<bool>("PlayButton", (y) => { onPlayGame = y; });

    }
    private void Start() 
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gameObject.SetActive(false);
        doubleJump = false;
        onPlayGame = false;
        wallJumpCheck = true;
    }

    private void FixedUpdate() 
    {
        TouchLRMovement();
        playerPositionY = transform.position.y;
    }


    void TouchLRMovement()
    {
        if (Input.touchCount > 0 && onPlayGame)
        {
            Touch touch = Input.GetTouch(0);


            if (touch.position.x > Screen.width / 2)
            {
                rb.AddForce(Vector2.right * movementSpeed, ForceMode2D.Force);
            }
            else if (touch.position.x < Screen.width / 2)
            {
                rb.AddForce(Vector2.left * movementSpeed, ForceMode2D.Force);;
            }
        }
    }

    public void JumpMovement()
    {
        if(!doubleJump && onPlayGame )
        {
            rb.velocity = Vector2.up * jumpHeight;
            doubleJump = true;
        }
        else if(doubleJump && onPlayGame)
        {
            rb.velocity = Vector2.up * (jumpHeight *2);
            doubleJump = false;
        }
    }
    
    public IEnumerator WallJump(Collision2D other) // ŞİMDİLİK DURSUN BU
    {   
        
        if(onPlayGame && wallJumpCheck)
        {
            float dir = transform.position.x - other.gameObject.transform.position.x;
            rb.AddForce(new Vector2(dir * 15,4f), ForceMode2D.Impulse);
            wallJumpCheck = false;
        }
        yield return new WaitForSeconds(1);
        wallJumpCheck = true;
    }


}
