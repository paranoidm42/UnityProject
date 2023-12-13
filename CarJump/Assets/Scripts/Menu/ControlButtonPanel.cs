
using UnityEngine;

public class ControlButtonPanel : MonoBehaviour
{
    private void Start()
    {
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(false);


    }
    private void OnEnable()
    {
        PlayerCollision.onGameOver += OnGameOverControl;
        GameOverMenu.onRetry += OnRetryControl;
        LevelManager.LastLevelHandler += OnLastLevel;

    }

    private void OnDisable()
    {
        PlayerCollision.onGameOver -= OnGameOverControl;
        GameOverMenu.onRetry -= OnRetryControl;
        LevelManager.LastLevelHandler -= OnLastLevel;
    }
    

    void OnGameOverControl()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }

    void OnRetryControl()
    {
        transform.GetChild(1).gameObject.SetActive(true);

    }

    void OnLastLevel()
    {
        transform.GetChild(0).gameObject.SetActive(false);

        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);

    }
}
