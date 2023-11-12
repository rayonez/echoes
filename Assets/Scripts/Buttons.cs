using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public GameObject loading,mainMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        //mainMenu.SetActive(false);
        loading.SetActive(true);
        Invoke("StartGame", 3f);
    }
    void StartGame() {
        SceneManager.LoadScene("Stage1");

    }
    public void GameQuit()
    {
        Application.Quit();
    }
}
