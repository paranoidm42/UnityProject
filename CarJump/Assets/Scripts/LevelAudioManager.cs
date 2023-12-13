
using UnityEngine;

public class LevelAudioManager : MonoBehaviour
{
    public AudioSource gameOverSFX;
    public AudioSource finishTFX;
    private void OnEnable()
    {
        PlayerCollision.onFinish += FinishSFX;
        PlayerCollision.onGameOver += GameOverSFX;

    }

    private void OnDisable()
    {
        PlayerCollision.onFinish -= FinishSFX;
        PlayerCollision.onGameOver -= GameOverSFX;
    }


    void FinishSFX()
    {
        if (!finishTFX.isPlaying)
            finishTFX.Play();
    }

    void GameOverSFX()
    {        
        if (!gameOverSFX.isPlaying)
            gameOverSFX.Play();

    }

}
