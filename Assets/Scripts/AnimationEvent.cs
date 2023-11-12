using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationEvent : MonoBehaviour
{
    public void StartOpeningLetter() {
        GameManager.instance.letter.SetBool("isOpened", true);
    }
    public void EnableLetter() {
        GameManager.instance.letter.gameObject.SetActive(true);
        GameManager.instance.letter.GetComponent<Button>().onClick.AddListener(GameManager.instance.OpenText);
    }

}
