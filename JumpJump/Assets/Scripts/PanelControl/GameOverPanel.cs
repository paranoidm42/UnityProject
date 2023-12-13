
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverPanel : MonoBehaviour
{
    Transform childPanel;
    bool onGameOver;
    private void OnEnable() 
    {
        EventManager.StartListening<bool>("GameOver", (x) => { onGameOver = x; });
    }
    private void OnDisable() 
    {
        EventManager.StopListening<bool>("GameOver", (x) => { onGameOver = x; });
    }

    private void Start() 
    {
        onGameOver = false;    
        childPanel = transform.GetChild(0);
        childPanel.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if(onGameOver)
        {
            childPanel.gameObject.SetActive(true);
        }

    }

    public void TryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
       
    }
}
