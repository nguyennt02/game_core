using TMPro;
using UnityEngine;

public class HeartSystem : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdountTxt;
    [SerializeField] TextMeshProUGUI amountHeartTxt;
    void Start()
    {
        CountdownSystem.Instance.OnEndLoopTimeSpin += AddHeart;
        GameManager.Instance.OnChangeCurrentHeart += UpdateAmountHeart;
        if (!GameManager.Instance.IsHeartFull)
        {
            CountdownSystem.Instance.PlayCountdown();
        }
    }
    void OnDestroy()
    {
        CountdownSystem.Instance.OnEndLoopTimeSpin -= AddHeart;
        GameManager.Instance.OnChangeCurrentHeart -= UpdateAmountHeart;
    }
    void UpdateAmountHeart(int amount)
    {
        amountHeartTxt.text = amount.ToString();
    }
    void AddHeart(int amount)
    {
        GameManager.Instance.CurrentHeart += amount;
    }
    void FixedUpdate()
    {
        if (!GameManager.Instance.IsHeartFull)
        {
            float ElapsedtimeSecounds = (float)CountdownSystem.Instance.ElapsedtimeSecounds;
            int minutes = Mathf.FloorToInt(ElapsedtimeSecounds / 60);
            int seconds = Mathf.FloorToInt(ElapsedtimeSecounds % 60);
            countdountTxt.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        }
        else
        {
            CountdownSystem.Instance.StopCountdown();
            countdountTxt.text = "Full";
        }
    }
    public void LostHeart()
    {
        GameManager.Instance.LostHeart();
    }
}
