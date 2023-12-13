
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text scoreText;
    [SerializeField]
    TMP_Text levelText;

    int scoreInt;
    int levelİnt;
    [SerializeField]
    bool GO;
    PlayerController  playerController;
    
    
    private void OnEnable() 
    {
        EventManager.StartListening<bool>("GameOver", OnGameOver);
    }
    private void OnDestroy() 
    {
        EventManager.StopListening<bool>("GameOver", OnGameOver);
        //PlayerCollision.GameOverEvent -= OnGameOver;
    }
    void Start()
    {
        //PlayerCollision.GameOverEvent += OnGameOver;
        
        gameObject.SetActive(true);
        transform.localPosition = new Vector2(0,  900);
        GO = false;
        levelİnt= 1;
        scoreInt = 1;
        playerController = PlayerController.Instance;
    }

    
    private void LateUpdate() 
    {
        
        if(!GO)
        {
            ScoreUp();
            LevelUp();
        }
        scoreText.text = "Score: " + scoreInt.ToString();
        levelText.text = "Level: " + levelİnt.ToString();
    }

    void ScoreUp()
    {
        scoreInt = (int)Time.timeSinceLevelLoad *  (int)playerController.playerPositionY * levelİnt;

    }
    
    void LevelUp()
    {
        levelİnt = Mathf.FloorToInt(playerController.playerPositionY / 70) + 1 ;
    }

    void OnGameOver(bool status)
    {
        GO = status;
        float scorePosition = Camera.main.transform.position.y / 2;
        transform.localPosition = new Vector2(0, 0);
    }
}
