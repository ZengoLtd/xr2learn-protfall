using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Events;

public class WelcomeUI : MonoBehaviour
{
    public List<UnityEvent> UnityEvents = new List<UnityEvent>();

    public float fadeTime = 1f;

    public LocalizedString nextText;
    public LocalizedString finishText;

    public List<GameObject> tutorialElements;
    public Button forwardButton;
    public Button backButton;

    public List<GameObject> teleporters = new List<GameObject>();

    CanvasGroup canvasGroup;
    int index = 0;
    int prev = 0;
    int pageNum = 1;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (backButton != null) backButton.gameObject.SetActive(false);
        SetTeleporters(false);
        for (int i = 0; i < tutorialElements.Count; i++)
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
        ShowUI();
    }

    public void ShowUI()
    {
        GetComponent<Canvas>().enabled = true;
        canvasGroup.LeanAlpha(1, fadeTime);
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

            if (index == tutorialElements.Count - 1)
            {
                forwardButton.GetComponentInChildren<TMP_Text>().text = finishText.GetLocalizedString();
            }
            else
            {
                forwardButton.GetComponentInChildren<TMP_Text>().text = nextText.GetLocalizedString();
            }
        }
        else
        {
            canvasGroup.LeanAlpha(0, fadeTime).setOnComplete(TurnOffCanvas);
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
