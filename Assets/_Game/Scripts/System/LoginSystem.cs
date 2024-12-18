using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LoginSystem : MonoBehaviour
{
    public static LoginSystem Instance { get; private set;}
    void Start()
    {
        Instance = this;
        Login();
    }
    void Login()
    {
        LoginDatas loginDatas = ConvertDataSystem.LoadDataFromFile<LoginDatas>(KeyString.NAME_FILE_LOGIN);
        if (loginDatas.datas == null)
        {
            loginDatas.datas = new();
        }
        else if(loginDatas.datas.Count > 0)
        {
            var day = loginDatas.datas[loginDatas.datas.Count - 1].day;
            if (IsLoginDayAt(day)) return;
        }
        DateTime today = DateTime.Now.Date;
        int3 itoday = new int3(today.Day, today.Month, today.Year);

        var data = new LoginData
        {
            day = itoday,
            isReceivedDailyReward = false
        };
        loginDatas.datas.Add(data);
        ConvertDataSystem.SaveToFile(loginDatas, KeyString.NAME_FILE_LOGIN);
    }

    bool IsLoginDayAt(int3 day)
    {
        DateTime today = DateTime.Now.Date;
        return day.x == today.Day && day.y == today.Month && day.z == today.Year;
    }

    public void ClearData()
    {
        LoginDatas loginDatas = ConvertDataSystem.LoadDataFromFile<LoginDatas>(KeyString.NAME_FILE_LOGIN);
        loginDatas.datas.Clear();
        ConvertDataSystem.SaveToFile(loginDatas, KeyString.NAME_FILE_LOGIN);
        Login();
    }
}
[Serializable]
public struct LoginDatas
{
    public List<LoginData> datas;
}
[Serializable]
public struct LoginData
{
    public int3 day;
    public bool isReceivedDailyReward;
}
