using System;
using System.Collections;
using UnityEngine;


public class PlayerCollision : MonoBehaviour
{
    [SerializeField]PlayerController playerController;

    public static event Action onFinish;
    public static event Action onGameOver;
    public static event Action onCameraShake;

    public static event Action onButton;


    [SerializeField] bool isFinished = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.transform.tag == "Finish")
        {
            if(!isFinished)
            {
                isFinished = true;
                onFinish?.Invoke();
                StartCoroutine(Timer(4));
                isFinished = false;
            }
           
   
        }
        else if (collision.transform.tag == "EnemySpace" && !playerController.isJumping)
        {
            onGameOver?.Invoke();
        }
        else if(collision.transform.tag == "Enemy")
        {
            onGameOver?.Invoke();
        }
        else if(collision.transform.tag == "RButton")
        {
            onButton?.Invoke();
        }
       
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "EnemySpace" && !playerController.isJumping)
        {
            onGameOver?.Invoke();
        }
        else if (collision.transform.tag == "Enemy")
        {
            onGameOver?.Invoke();
        } 
        else if(collision.transform.tag == "RButton")
        {
            onButton?.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == "Wall")
        {
            StartCoroutine(Timer(1));
            onCameraShake?.Invoke();

        }
        
    }


    IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
