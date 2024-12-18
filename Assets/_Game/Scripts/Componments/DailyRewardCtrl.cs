using UnityEngine;
using UnityEngine.UI;

public class DailyRewardCtrl : MonoBehaviour
{
    public enum DayState
    {
        Lock,
        Unlock,
        Received
    }
    public RewardData[] rewardDatas;
    public DayState currentState;
    [SerializeField] Image iconImg;
    public void Lock()
    {
        currentState = DayState.Lock;
        iconImg.color = Color.gray;
    }
    public void Unlock()
    {
        currentState = DayState.Unlock;
        iconImg.color = Color.blue;
    }
    public void Received()
    {
        currentState = DayState.Received;
        iconImg.color = Color.green;
    }
}
