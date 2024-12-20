using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public Action<int> OnChangeCurrentHeart;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int maxHeart { get => 5; }

    public int CurrentHeart
    {
        get => PlayerPrefs.GetInt(KeyString.KEY_CURRENT_HEART, maxHeart);
        set
        {
            if (value > maxHeart) value = maxHeart;
            if (value < 0) value = 0;
            PlayerPrefs.SetInt(KeyString.KEY_CURRENT_HEART, value);
            OnChangeCurrentHeart?.Invoke(value);
        }
    }
    public bool IsHeartFull => CurrentHeart == maxHeart;
    public bool IsHeartEmpty => CurrentHeart == 0;

    public void LostHeart()
    {
        if (IsHeartFull)
            CountdownSystem.Instance.StartCountdown();
        CurrentHeart--;
    }
}
