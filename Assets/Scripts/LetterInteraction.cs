using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetterInteraction : MonoBehaviour
{
    public GameObject envelopeA;
    public GameObject envelopeB;
    public GameObject grid;
    public GameObject letter;
    public TextMeshProUGUI script;
    public TextMeshProUGUI guide;
    
    

    public void EnvelopeAOpen()
    {
        envelopeA.SetActive(false);
        envelopeB.SetActive(true);
    }

    public void EnvelopeBOpen()
    {
        envelopeB.SetActive(false);
        grid.SetActive(true);

        script.text = "";
        guide.text = "CLICK TO PICK UP/DOWN AND MOVE TO OBJECTS\nALSO CLICK AND PRESS 'R' TO ROTATE OBJECTS";
    }

    public void LetterClose()
    {
        letter.SetActive(false);
    }
}
