using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;


    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;

        Hide();
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStartActive())
        {
            Show();
        }
        else
        {
            Hide(); 
        }
    }

    private void Update()
    {
        countdownText.text = GameManager.Instance.GetCountdownToStartTimer().ToString("#");
    }

    private void Hide()
    {
        countdownText.gameObject.SetActive(false);
    }

    private void Show()
    {
        countdownText.gameObject.SetActive(true);
    }
}
