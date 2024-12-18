using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ShowItemSystem : MonoBehaviour
{
    public static ShowItemSystem Instance { get; private set; }
    [SerializeField] Transform listItem;
    [SerializeField] float duration;
    void Awake()
    {
        Instance = this;
    }
    void OnEnable()
    {
        Setup();
    }
    void Setup()
    {
        listItem.localScale = Vector2.one * Mathf.Pow(0.9f, listItem.childCount);
    }
    public void ShowItemsAt(RewardData[] itemDatas)
    {
        RewardData[] rewardDatas = CombineItem(itemDatas);
        foreach (var reward in rewardDatas)
        {
            ShowItemAt(reward);
        }
    }
    public void ShowItemAt(RewardData itemData)
    {
        GameObject item = PoolingObject(listItem);
        ItemCtrl itemCtrl = item.GetComponent<ItemCtrl>();
        itemCtrl.InjectData(itemData);
        itemCtrl.Setup();
        itemCtrl.duration = duration;
        itemCtrl.Show();
    }

    GameObject PoolingObject(Transform parent)
    {
        foreach (Transform child in parent)
        {
            if (!child.gameObject.activeInHierarchy)
            {
                child.gameObject.SetActive(true);
                return child.gameObject;
            }
        }
        var item = Instantiate(parent.GetChild(0).gameObject, parent);
        item.SetActive(true);
        return item;
    }

    RewardData[] CombineItem(RewardData[] itemData)
    {
        Dictionary<TypeItem, RewardData> dic_items = new();
        foreach (var item in itemData)
        {
            if (dic_items.ContainsKey(item.typeItem))
                dic_items[item.typeItem].amount += item.amount;
            else
                dic_items.Add(item.typeItem, CloneRewarData(item));
        }
        List<RewardData> rewardDatas = new();
        foreach (var item in dic_items)
        {
            rewardDatas.Add(item.Value);
        }
        return rewardDatas.ToArray();
    }

    RewardData CloneRewarData(RewardData rewardData)
    {
        // Tạo một bản sao mới của RewardData
        RewardData clonedRewardData = ScriptableObject.CreateInstance<RewardData>();

        // Sao chép giá trị từ rewardData gốc
        clonedRewardData.typeItem = rewardData.typeItem;
        clonedRewardData.icon = rewardData.icon;
        clonedRewardData.amount = rewardData.amount;
        clonedRewardData.percent = rewardData.percent;

        return clonedRewardData;
    }

    public void Claim()
    {
        UIManager.Instance.HideModal();
        foreach (Transform item in listItem)
        {
            if (item.TryGetComponent(out ItemCtrl itemCtrl))
            {
                itemCtrl.Hide().OnComplete(() => itemCtrl.gameObject.SetActive(false));
                // nhan qua
            }
        }
    }
}
