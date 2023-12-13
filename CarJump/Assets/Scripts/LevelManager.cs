using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] string levels = "Level";

    [SerializeField] int levelNum;

    [SerializeField] GameObject levelObj;
    [SerializeField] GameObject prefab;

    public static event Action LastLevelHandler;

    void OnEnable()
    {
        PlayerCollision.onFinish += NextLevel;
    }
    void OnDisable()
    {
        PlayerCollision.onFinish -= NextLevel;
    }


   private void Start()
    {
        FirstLevelLoad();
        levelObj = GameObject.FindGameObjectWithTag("Level");
    }
    private void Update()
    {
        if(levelObj == null)
            levelObj = GameObject.FindGameObjectWithTag("Level");

    }

    void NextLevel()
    {
        levels = "Level"+NumberParse();
        DestroyCurrentPrefab();
        StartCoroutine(PrefabCall());
        LevelUnlock();
    }

    IEnumerator PrefabCall()
    {
        prefab = Resources.Load<GameObject>(levels);
        if(prefab != null)
        {
            
            GameObject instantiatedPrefab = Instantiate(prefab);
            instantiatedPrefab.transform.position = new Vector3(0f, 0f, 0f); 
            instantiatedPrefab.transform.rotation = Quaternion.identity;
            yield return new WaitForSeconds(3f);
            Debug.Log("LEVEL GEÇ");
        }
        else 
            LastLevelCheck();
    }

    void LastLevelCheck() // SON SEVİYEE OLACAKLAR
    {
        LastLevelHandler?.Invoke();
    }

    void DestroyCurrentPrefab()
    {
        Destroy(levelObj);
    }

    string NumberParse()
    {
        string levelParse = levelObj.name;
        string numberStr = new string(levelParse.Where(char.IsDigit).ToArray());
        levelNum = Int32.Parse(numberStr) + 1;
        return levelNum.ToString();
    }


    void FirstLevelLoad()
    {

        string firsPrefabName = "Level"+PlayerPrefs.GetString("levelChangeName");

        Debug.Log(firsPrefabName);
        GameObject firsPrefab = Resources.Load<GameObject>(firsPrefabName);
        GameObject instantiatedPrefab = Instantiate(firsPrefab);
        instantiatedPrefab.transform.position = new Vector3(0f, 0f, 0f);
        instantiatedPrefab.transform.rotation = Quaternion.identity;
    }


    void LevelUnlock()
    {

        if (levelNum > PlayerPrefs.GetInt("unlockedLevel"))
        {
            PlayerPrefs.SetInt("unlockedLevel", levelNum);
        }
        
    }
}
