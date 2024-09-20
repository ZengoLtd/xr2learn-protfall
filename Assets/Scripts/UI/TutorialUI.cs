using BNG;
using MEM;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class TutorialUI : MonoBehaviour
{
    public List<UnityEvent> UnityEvents = new List<UnityEvent>();

    public float fadeTime = 1f;
    public bool isSceneSelector;
    public List<GameObject> tutorialElements;
    public UnityEngine.UI.Button forwardButton;
    public UnityEngine.UI.Button backButton;
    public List<GameObject> teleporters = new List<GameObject> ();
    public TMP_Text pageNumbers;
    int index = 0;
    int prev = 0;
    int pageNum = 1;
   
    void Awake()
    {
        if (backButton != null) backButton.gameObject.SetActive(false);
        SetTeleporters(false);
        GetComponent<CanvasGroup>().LeanAlpha(0, 0f);
        GetComponent<Canvas>().enabled = true;
        index = 0;
        pageNum = 1;
        pageNumbers.text = pageNum.ToString() + " / " +  tutorialElements.Count.ToString();
        
        for (int i= 0; i < tutorialElements.Count; i++)
        {
            if (i == 0)
            {
                tutorialElements[i].SetActive(true);
            }
            else
            {
                tutorialElements[i].SetActive(false);
            }
        }
        if (isSceneSelector)
        {
            ShowUI();
        }
    }

    public void ShowUI()
    {
        GetComponent<Canvas>().enabled = true;
        GetComponent<CanvasGroup>().LeanAlpha(1, fadeTime);
    }

    public void OnClick(int direction)
    {
        prev = index;
        index += direction;
        pageNum = index + 1;
        if (index < tutorialElements.Count)
        {
            if (index == 0)
            {
                backButton.gameObject.SetActive(false);
            }
            else
            {
                backButton.gameObject.SetActive(true);
            }
            tutorialElements[prev].SetActive(!tutorialElements[prev].activeSelf);
            tutorialElements[index].SetActive(true);

            if (index == tutorialElements.Count-1)
            {
                forwardButton.GetComponentInChildren<TMP_Text>().text = "Befejezés";
            }
            else
            {
                forwardButton.GetComponentInChildren<TMP_Text>().text = "Következő";
            }
            pageNumbers.text = pageNum.ToString() + " / " +  tutorialElements.Count.ToString();
        }
        else
        {
            GetComponent<CanvasGroup>().LeanAlpha(0, fadeTime).setOnComplete(TurnOffCanvas);
            foreach (var action in UnityEvents)
            {
                action.Invoke();
            }
            SetTeleporters(true);

            
        }
    }

    void SetTeleporters(bool state)
    {
        if (teleporters.Count > 0)
        {
            foreach (GameObject obj in teleporters)
            {
                obj.SetActive(state);
            }
        }
    }

    void TurnOffCanvas()
    {
        this.gameObject.SetActive(false);
    }
}
