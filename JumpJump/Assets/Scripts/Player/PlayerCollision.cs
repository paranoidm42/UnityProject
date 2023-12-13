
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{

    PlayerController  playerController;
    PlayerBuff playerBuff;

    
    private void Start() 
    {
        playerController = PlayerController.Instance;    
        playerBuff = gameObject.GetComponent<PlayerBuff>();
        
    
    }
    
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.transform.tag == "LR")
        {
            StartCoroutine(playerController.WallJump(other));
        }
        
        if((other.transform.tag == "Platform" || other.transform.tag == "Ground"))
        {
            playerController.JumpMovement();
        }

        if(other.transform.tag == "Trap")
        {    
            EventManager.TriggerEvent<bool>("GameOver",true);
            gameObject.SetActive(false);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.transform.tag == "JBost")
        {
            StartCoroutine(playerBuff.AppleBuff(other));
        }        
    }


}
