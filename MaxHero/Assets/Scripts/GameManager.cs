using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    float timeDelayRestart;
    float time;

    public static GameManager instance;

    GameObject foxObject;

    public int foxHp;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (FoxController.Instance)
        {
            if (FoxController.Instance.dead)
            {
                time += Time.deltaTime;
                if (time > timeDelayRestart)
                {
                    foxHp = 100;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }    
    }

    private void DestroyFoxObject()
    {

    }

    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
