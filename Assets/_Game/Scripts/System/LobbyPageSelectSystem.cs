using System;
using UnityEngine;

public class LobbyPageSelectSystem : MonoBehaviour
{
    [SerializeField] LobbyPageSelectCtrl[] lobbyPageSelectCtrls;
    [Header("Event")]
    public static Action<int> OnSelect;
    void Start()
    {
        Init();
    }
    void Init()
    {
        SelectAt(1);
    }
    public void SelectAt(int index)
    {
        for (int i = 0; i < lobbyPageSelectCtrls.Length; i++)
        {
            if (lobbyPageSelectCtrls[i].isLock)
                lobbyPageSelectCtrls[i].Lock();
            else if (i == index)
            {
                OnSelect?.Invoke(i);
                lobbyPageSelectCtrls[i].Selecting();
            }
            else
                lobbyPageSelectCtrls[i].Unchecking();
        }
    }
}
