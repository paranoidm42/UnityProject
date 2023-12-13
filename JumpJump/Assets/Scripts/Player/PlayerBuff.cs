using System.Collections;
using UnityEngine;

public class PlayerBuff : MonoBehaviour
{
    PlayerController playerController;

    void Start()
    {
        playerController = PlayerController.Instance; 
    }

    void Update()
    {
        
    }
    public IEnumerator AppleBuff(Collider2D other)
    {
        float swapJumpHeight= playerController.jumpHeight;
        playerController.jumpHeight += 5;
        other.gameObject.SetActive(false);
        yield return new WaitForSeconds(5);
        playerController.jumpHeight = 4;
        Debug.Log("Değdi");
    }
}
