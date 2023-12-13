using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    [Header("Profile")]
    [SerializeField] private string profileId;
    [Header("Content")]
    [SerializeField] private GameObject noDataContent;
    [SerializeField] private GameObject hasDataContent;
    [SerializeField] private TMP_Text playtimeTMP;
    [SerializeField] private Button slotButton;
    [Header("Clear Data Button")]
    [SerializeField] private Button clearButton;

    public bool hasData { get; private set; } = false;

    public void SetData(GameData data)
    {
        // there's no data for this profileId
        if (data == null)
        {
            hasData = false;
            noDataContent.SetActive(true);
            hasDataContent.SetActive(false);
            clearButton.gameObject.SetActive(false);
        }
        // there is data for this profileId
        else
        {
            hasData = true;
            noDataContent.SetActive(false);
            hasDataContent.SetActive(true);
            clearButton.gameObject.SetActive(true);

            // set the playtime
            TimeSpan timeSpan = TimeSpan.FromSeconds(data.playtimeSec);
            playtimeTMP.text = "Playtime (H:M): " + timeSpan.Hours.ToString() + ":" + timeSpan.Minutes.ToString();
        }
    }

    public string GetProfileId()
    {
        return this.profileId;
    }

    public void SetInteractable(bool isInteractable)
    {
        slotButton.interactable = isInteractable;
        clearButton.interactable = isInteractable;
    }
}
