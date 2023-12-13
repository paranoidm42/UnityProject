
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuLevelLoad : MonoBehaviour
{


    public void LevelLoad()
    {
        PlayerPrefs.SetString("levelChangeName", transform.name);
        SceneManager.LoadScene(1);
    }
    
}
