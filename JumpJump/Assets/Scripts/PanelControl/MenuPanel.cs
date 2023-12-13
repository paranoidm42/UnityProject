using System.Threading.Tasks;
using UnityEngine;
using TMPro;
using System;
public class MenuPanel : MonoBehaviour
{
    Transform childPanel;
    PlayerController playerController;

    public TMP_Text timer;

    void Start()
    {
        playerController = PlayerController.Instance;
        childPanel = transform.GetChild(0);
        childPanel.gameObject.SetActive(true);
    }


    public void PlayGame()
    {
        
        childPanel.gameObject.SetActive(false);
        playerController.gameObject.SetActive(true);
        StartTimerAsync();
    }

    async void StartTimerAsync()
    {
        timer.gameObject.SetActive(true);
        for(int i = 3; i > 0; i--)
        {
            timer.text = i.ToString();
            await Wait(0.5f);
        }
        timer.text = "Let's Start";
        await Wait(0.5f);
        EventManager.TriggerEvent<bool>("PlayButton", true);
        timer.gameObject.SetActive(false);
        
    }

    async Task Wait(float time)
    {
        await Task.Delay(TimeSpan.FromSeconds(time));
    }
}
