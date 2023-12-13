using UnityEngine;
using UnityEngine.UI;


public class MenuLevelManager : MonoBehaviour
{
    [SerializeField] Button[] buttons;

    [SerializeField] int unlockedLevel;
    void Start()
    {
        unlockedLevel = PlayerPrefs.GetInt("unlockedLevel", 1);

        for(int i = 0; i < buttons.Length; i++) 
        {
            buttons[i].interactable = false;
        }
        
        int maxIndex = Mathf.Min(unlockedLevel, buttons.Length);

        for (int i = 0; i < maxIndex; i++) 
        {
            buttons[i].interactable = true;
        }


    }

}
