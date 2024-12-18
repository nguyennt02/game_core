using System;
using UnityEngine;

public class CountdownSystem : MonoBehaviour
{
    public static CountdownSystem Instance {get; private set;}
    [SerializeField] int maxTimeInSeconds = 1200;
    double elapsedtimeSecounds;
    public double ElapsedtimeSecounds { get; private set; }
    public Action<int> OnEndLoopTimeSpin;
    bool isPause = true;
    DateTime startTime;
    void Start()
    {
        Instance = this;
        if (PlayerPrefs.HasKey(KeyString.KEY_EXIt_TIME))
        {
            long exitTime = long.Parse(PlayerPrefs.GetString(KeyString.KEY_EXIt_TIME));
            startTime = DateTime.FromBinary(exitTime);
        }
        else
        {
            startTime = DateTime.Now;
        }
    }
    public void StartCountdown()
    {
        startTime = DateTime.Now;
        PlayerPrefs.SetString(KeyString.KEY_EXIt_TIME, startTime.ToBinary().ToString());
        isPause = false;
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
        elapsedtimeSecounds = elapsedtime.TotalSeconds;
        if (elapsedtimeSecounds > maxTimeInSeconds)
        {
            int amountSpin = (int)elapsedtimeSecounds / maxTimeInSeconds;
            double timeRmaining = (double)elapsedtimeSecounds % maxTimeInSeconds;
            OnEndLoopTimeSpin?.Invoke(amountSpin);
            StartCountdown();
            startTime -= TimeSpan.FromSeconds(timeRmaining);
        }
    }
}
