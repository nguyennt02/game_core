using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCtrl : MonoBehaviour
{
    public RewardData data;
    public float duration;
    [SerializeField] Image iconImg;
    [SerializeField] TextMeshProUGUI amount;
    public void InjectData(RewardData data)
    {
        this.data = data;
    }
    public void Setup()
    {
        iconImg.sprite = data.icon;
        amount.text = "x" + data.amount.ToString();
    }

    public void Show()
    {
        iconImg.transform.localScale = Vector2.zero;
        iconImg.transform.DOScale(Vector2.one, duration).SetEase(Ease.OutBounce);
    }

    public void Hide()
    {
        iconImg.transform.localScale = Vector2.one;
        iconImg.transform.DOScale(Vector2.zero, duration).SetEase(Ease.InBounce);
    }
}
