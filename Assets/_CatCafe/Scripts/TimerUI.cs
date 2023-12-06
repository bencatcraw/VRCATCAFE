using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    // Update is called once per frame
    void Update()
    {
        timerText.text = "Time Left: " + GameManager.Instance.GetGameTimer().ToString("#") + " seconds";
    }
}
