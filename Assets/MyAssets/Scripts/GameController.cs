using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    [Header("Itens level 1")]
    public GameObject FirstCat;
    public GameObject catDialog;
    public GameObject catDialog2;

    public static GameController instance = null;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        enableDialog(catDialog2, false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public static int getLevelIndex() //TODOOOO: corrigir dps da interface
    {
        string level = SceneManager.GetActiveScene().name;
        string levelNumber = level.Substring(level.Length - 1);
        return int.Parse(levelNumber) - 1; //arrays comecam em 0...
    }

    public void nextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void newChance(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void enableTutorialCat(bool state)
    {
        FirstCat.SetActive(state);
    }
    public void enableDialog(GameObject dialog, bool stts)
    {
        dialog.SetActive(stts);
    }
}
