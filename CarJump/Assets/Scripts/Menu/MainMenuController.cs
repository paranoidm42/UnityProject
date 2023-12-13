
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    private void Start()
    {
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(false);

    }

    public void PlayButton()
    {
        if(PlayerPrefs.GetInt("unlockedLevel") == 0) 
        {
            PlayerPrefs.SetInt("unlockedLevel", 1);
        }
        
        string lastLevel = PlayerPrefs.GetInt("unlockedLevel").ToString();

        if(PlayerPrefs.GetInt("unlockedLevel") == 11) //buna ozel komut yazılmalı
            lastLevel = "10";


        PlayerPrefs.SetString("levelChangeName", lastLevel);
        SceneManager.LoadScene(1);
    }

    public void LevelButton()
    {
        
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(3).gameObject.SetActive(true);
    }

    public void SettingButton()
    {
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);
    }

    public void BackButton()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
    }

        public void HowTo()
    {
        
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(4).gameObject.SetActive(true);
    }

}
