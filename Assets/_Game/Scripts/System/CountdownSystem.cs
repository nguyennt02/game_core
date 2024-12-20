using System;
using UnityEngine;

public class CountdownSystem : MonoBehaviour
{
    public static CountdownSystem Instance { get; private set; }
    [SerializeField] int maxTimeInSeconds = 1200;
    double _elapsedtimeSecounds;
    public double ElapsedtimeSecounds { get => _elapsedtimeSecounds;}
    public Action<int> OnEndLoopTimeSpin;
    bool isPause = true;
    DateTime startTime;
    void Awake()
    {
        Instance = this;
    }
    public void StartCountdown()
    {
        startTime = DateTime.Now;
        PlayerPrefs.SetString(KeyString.KEY_EXIT_TIME, startTime.ToBinary().ToString());
        isPause = false;
    }

    public void PlayCountdown()
    {
        isPause = false;
        if (PlayerPrefs.HasKey(KeyString.KEY_EXIT_TIME))
        {
            long exitTime = long.Parse(PlayerPrefs.GetString(KeyString.KEY_EXIT_TIME));
            startTime = DateTime.FromBinary(exitTime);
        }
        else
        {
            startTime = DateTime.Now;
        }
    }

    public void StopCountdown()
    {
        isPause = true;
    }

    void FixedUpdate()
    {
        LoopTime();
    }

    void LoopTime()
    {
        if (isPause) return;
        TimeSpan elapsedtime = DateTime.Now - startTime;
        _elapsedtimeSecounds = maxTimeInSeconds - elapsedtime.TotalSeconds;
        if (elapsedtime.TotalSeconds > maxTimeInSeconds)
        {
            int amountSpin = (int)elapsedtime.TotalSeconds / maxTimeInSeconds;
            double timeRmaining = (double)elapsedtime.TotalSeconds % maxTimeInSeconds;
            OnEndLoopTimeSpin?.Invoke(amountSpin);
            StartCountdown();
            startTime -= TimeSpan.FromSeconds(timeRmaining);
        }
    }
}
