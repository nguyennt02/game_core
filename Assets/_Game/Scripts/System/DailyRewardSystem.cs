using UnityEngine;

public class DailyRewardSystem : MonoBehaviour
{
    [SerializeField] DailyRewardCtrl[] dailyRewardCtrls;
    void Start()
    {
        Setup();
    }

    void Setup()
    {
        LoginDatas loginDatas = ConvertDataSystem.LoadDataFromFile<LoginDatas>(KeyString.NAME_FILE_LOGIN);
        var amountDayLogin = loginDatas.datas.Count;
        if (amountDayLogin > dailyRewardCtrls.Length)
        {
            LoginSystem.Instance.ClearData();
            loginDatas = ConvertDataSystem.LoadDataFromFile<LoginDatas>(KeyString.NAME_FILE_LOGIN);
            amountDayLogin = loginDatas.datas.Count;
        }

        for (int i = 0; i < dailyRewardCtrls.Length; i++)
        {
            if (i < amountDayLogin)
            {
                if (loginDatas.datas[i].isReceivedDailyReward)
                    dailyRewardCtrls[i].Unlock();
                else
                    dailyRewardCtrls[i].Received();
            }
            else
            {
                dailyRewardCtrls[i].Lock();
            }
        }
    }

    public void CLick(int index)
    {
        switch (dailyRewardCtrls[index].currentState)
        {
            case DailyRewardCtrl.DayState.Lock:
                break;
            case DailyRewardCtrl.DayState.Unlock:
                break;
            case DailyRewardCtrl.DayState.Received:
                UpdateData(index);
                dailyRewardCtrls[index].Unlock();
                ShowItemSystem.Instance.ShowItemsAt(dailyRewardCtrls[index].rewardDatas);
                UIManager.Instance.ShowModal(KeyString.NAME_POPUP_SHOW_ITEM);
                break;
        }
    }

    void UpdateData(int index)
    {
        LoginDatas loginDatas = ConvertDataSystem.LoadDataFromFile<LoginDatas>(KeyString.NAME_FILE_LOGIN);
        var data = loginDatas.datas[index];
        data.isReceivedDailyReward = true;
        loginDatas.datas[index] = data;
        ConvertDataSystem.SaveToFile(loginDatas, KeyString.NAME_FILE_LOGIN);
    }
}
