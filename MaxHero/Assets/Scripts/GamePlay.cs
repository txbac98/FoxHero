using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePlay : MonoBehaviour
{
    [SerializeField]
    float timeDelayRestart;
    float time;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (FoxController.Instance.dead)
        {
            time += Time.deltaTime;
            if (time > timeDelayRestart)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }   
    }
}
