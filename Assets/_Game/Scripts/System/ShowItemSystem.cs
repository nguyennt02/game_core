using UnityEngine;

public class ShowItemSystem : MonoBehaviour
{
    public static ShowItemSystem Instance { get; private set; }
    [SerializeField] ItemCtrl itemCtrlPrefab;
    [SerializeField] Transform listItem;
    [SerializeField] float duration;
    void Awake()
    {
        Instance = this;
    }
    public void ShowItemsAt(RewardData[] itemDatas)
    {
        foreach (var itemData in itemDatas)
        {
            ShowItemAt(itemData);
        }
    }
    public void ShowItemAt(RewardData itemData)
    {
        var item = Instantiate(itemCtrlPrefab, listItem);
        item.InjectData(itemData);
        item.Setup();
        item.duration = duration;
        item.Show();
    }

    public void HideItemAt()
    {
        foreach (Transform item in listItem)
        {
            if (item.TryGetComponent(out ItemCtrl itemCtrl))
            {
                itemCtrl.Hide();
            }
        }
    }

    public void Claim()
    {
        UIManager.Instance.HideModal();

        foreach (Transform item in listItem)
        {
            if (item.TryGetComponent(out ItemCtrl itemCtrl))
            {
                // nhan qua
            }
        }
    }
}
