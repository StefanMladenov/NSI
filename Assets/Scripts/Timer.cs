using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float seconds = 0;
    public UnityEvent onTimerExpired;
    private float startTime;
    private bool isTicking;
    public Text timeText;
    // Start is called before the first frame update
    void Start()
    {
        seconds = MathOperationsSetupController.duration;
        startTime = Time.time;
        isTicking = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isTicking)
        {
            timeText.text = TimeSpan.FromSeconds(startTime + seconds - Time.time).Minutes.ToString() + ":" + TimeSpan.FromSeconds(startTime + seconds - Time.time).Seconds.ToString();
        }
        else
        {
            timeText.text = "Expired";
        }

        if (isTicking && Time.time >= startTime + seconds)
        {
            onTimerExpired.Invoke();
            isTicking = false;
        }
    }
}
