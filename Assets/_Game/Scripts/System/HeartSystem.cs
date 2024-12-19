using UnityEngine;

public class HeartSystem : MonoBehaviour
{
    void Start()
    {
        CountdownSystem.Instance.OnEndLoopTimeSpin += AddHeart;
        if (!GameManager.Instance.IsHeartFull)
        {
            CountdownSystem.Instance.PlayCountdown();
        }
    }
    void OnDestroy()
    {
        CountdownSystem.Instance.OnEndLoopTimeSpin -= AddHeart;
    }
    void AddHeart(int amount)
    {
        GameManager.Instance.CurrentHeart += amount;
        Debug.Log("heart" + GameManager.Instance.CurrentHeart);
    }
    double cc;
    void FixedUpdate()
    {
        if (!GameManager.Instance.IsHeartFull)
        {
            if (cc != (int)CountdownSystem.Instance.ElapsedtimeSecounds)
            {
                Debug.Log("time" + CountdownSystem.Instance.ElapsedtimeSecounds);
            }
            cc = (int)CountdownSystem.Instance.ElapsedtimeSecounds;
        }
        else
        {
            CountdownSystem.Instance.StopCountdown();
        }
    }
}
