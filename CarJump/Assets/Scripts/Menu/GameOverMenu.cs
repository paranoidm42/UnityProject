
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class GameOverMenu : MonoBehaviour
{

    public static event Action onRetry;
    private void OnEnable()
    {
        PlayerCollision.onGameOver += OnGameOverMenu;
    }

    private void OnDisable()
    {
        PlayerCollision.onGameOver -= OnGameOverMenu;
    }
    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(false); //gameover false
    }

    public void Retry()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        onRetry?.Invoke();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    void OnGameOverMenu()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
