using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LuckyWheelCtrl : MonoBehaviour
{
    [Header("Data")]
    public TypeItem typeItem;
    public Sprite icon;
    public int amount;
    [Range(0, 1)]
    public float percent;
    [Header("Renferent")]
    [SerializeField] Image iconImg;
    [SerializeField] TextMeshProUGUI amountTxt;

    public void SetUp()
    {
        iconImg.sprite = icon;
        amountTxt.text = amount.ToString();
    }

    public void Select()
    {
        iconImg.transform.DOScale(Vector3.one * 1.3f, 0.3f).SetEase(Ease.OutElastic);
    }

    public void NonSelect()
    {
        transform.localScale = Vector3.one;
    }

    public void Spin()
    {

    }
}
public enum TypeItem
{
    Coin,
    Gem
}
