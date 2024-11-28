using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LuckWheelSystem : MonoBehaviour
{
    [SerializeField] Transform wheel;
    [Range(1, 5)]
    [SerializeField] int spinAmount;
    [SerializeField] float duration;
    [SerializeField] LuckyWheelCtrl[] luckyWheelCtrls;
    float startAngles;
    void Start()
    {
        SetUpItem();
        //Spin();
    }
    public void SetUpItem()
    {
        startAngles = wheel.transform.eulerAngles.z % 360;
        var eulerAngles = startAngles;
        var step = 360 / luckyWheelCtrls.Length;

        foreach (var luckyWheelCtrl in luckyWheelCtrls)
        {
            luckyWheelCtrl.NonSelect();
            luckyWheelCtrl.SetUp();
            luckyWheelCtrl.transform.eulerAngles = new Vector3(0, 0, eulerAngles);
            eulerAngles += step;
        }
    }

    public void Spin()
    {
        var startValue = wheel.transform.eulerAngles.z % 360;
        var endValue = startValue + 360;
        DOTween.To(() => startAngles,
        value => wheel.transform.eulerAngles = new Vector3(0, 0, value),
        endValue, duration)
        .SetEase(Ease.Linear)
        .SetLoops(-1, LoopType.Incremental);
    }

    public void Stop()
    {
        var itemIndex = RandomIndexItem();
        var step = 360 / luckyWheelCtrls.Length;
        var eulerAngles = itemIndex * step;
        var endValue = 360 * spinAmount + (startAngles - eulerAngles) % 360;

        DOTween.KillAll();
        DOTween.To(() => wheel.transform.eulerAngles.z % 360,
        value => wheel.transform.eulerAngles = new Vector3(0, 0, value),
        endValue, endValue / 360)
        .SetEase(Ease.OutSine)
        .OnComplete(()=> Debug.Log(luckyWheelCtrls[itemIndex].amount));
    }


    int RandomIndexItem()
    {
        float randomValue = Random.Range(0f, 1f);
        Debug.Log(randomValue);
        float cumulativeProbability = 0;
        for (int i = 0; i < luckyWheelCtrls.Length; i++)
        {
            cumulativeProbability += luckyWheelCtrls[i].percent;
            if (randomValue <= cumulativeProbability)
                return i;
        }
        return -1;
    }
}
