using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    public Sprite[] sprit;
    public Image BGsprit;
    // Start is called before the first frame update
    void Start()
    {
        if(BGsprit !=null)
        BGsprit.sprite = sprit[(int)Random.Range(0, sprit.Length)];


    }

    // Update is called once per frame
    void Update()
    {
        
    
    }

    public void GetLevelData(string levelName)
    {
        StaticManager.levelName = levelName;

       // StaticManager.levelName1 = gameObject.name;
    }

    public void LoadScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void Exit()
    {
#if(UNITY_EDITOR)
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }


}
