using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public Animator envelope;
    int clickCounter = 0;
    public Animator letter;

    public GameObject letterWithText,brokenPieces,hintObj,finalFrameObj,gameEnd;
    public int currentArrangedCounter = 0;
    public int counterToWin=6;

    public Image helperImg;
    public Sprite helper2,helper3;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    public void ClickedOnEvn() {
        Debug.Log("Clicked");
        if (clickCounter == 0)
        {
            clickCounter=1;
            envelope.SetBool("IsOpened1", true);
        }
        else if (clickCounter == 1)
        {
            clickCounter = 2;
            envelope.SetBool("isOpened2", true);
            helperImg.sprite = helper2;
        }
        else {

        }
    }
    public void OpenText()
    {
        letterWithText.SetActive(true);
    }
    public void CloseTextAndPlaceAllPieces() {
        helperImg.sprite = helper3;
        letterWithText.SetActive(false);
        brokenPieces.SetActive(true);
        hintObj.SetActive(true);
        letter.GetComponent<Button>().onClick.RemoveAllListeners();

    }
    public void CheckForGameFinished() {
        if (currentArrangedCounter == counterToWin) {
            Debug.Log("You Win");
            finalFrameObj.SetActive(true);
            Invoke("GameEndPlease",3f);
        }
    }
    void GameEndPlease() {
        gameEnd.SetActive(true);
    }

    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // Update is called once per frame
    void Update()
    {
        
    }

}
