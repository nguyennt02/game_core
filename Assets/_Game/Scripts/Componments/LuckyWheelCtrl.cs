using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LuckyWheelCtrl : MonoBehaviour
{
    [Header("Data")]
    public RewardData data;

    [Header("Renferent")]
    [SerializeField] Image iconImg;
    [SerializeField] TextMeshProUGUI amountTxt;
    public void InjectData(RewardData data)
    {
        this.data = data;
    }

    public void SetUp()
    {
        iconImg.sprite = data.icon;
        amountTxt.text = data.amount.ToString();
    }

    public void Select()
    {
        transform.DOScale(Vector3.one * 1.3f, 0.3f).SetEase(Ease.OutElastic);
    }

    public void NonSelect()
    {
        transform.localScale = Vector3.one;
    }
}
public enum TypeItem
{
    Coin,
    Gem
}
