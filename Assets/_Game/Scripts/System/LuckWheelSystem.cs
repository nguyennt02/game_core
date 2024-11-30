using System;
using DG.Tweening;
using UnityEngine;

public class LuckWheelSystem : MonoBehaviour
{
    [SerializeField] Transform wheel;
    [Range(1, 5)]
    [SerializeField] int spinAmount;
    [Range(1, 5)]
    [SerializeField] float duration;
    [SerializeField] LuckyWheelCtrl[] luckyWheelCtrls;
    float startAngles;
    int itemIndex = 0;
    public bool spin = true;
    public bool stop = true;
    public Action<LuckyWheelCtrl> onAfterStop;
    public Action<bool> onBeforeSpin;
    public Action<bool> onBeforeStop;
    void Start()
    {
        SetUpItem();
        Spin();
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
        onBeforeSpin?.Invoke(spin);
        if (!spin) return;
        luckyWheelCtrls[itemIndex].NonSelect();
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
        onBeforeStop?.Invoke(stop);
        if (!stop) return;
        itemIndex = RandomIndexItem();
        var step = 360 / luckyWheelCtrls.Length;
        var eulerAngles = itemIndex * step;
        var endValue = 360 * spinAmount + (startAngles - eulerAngles) % 360;

        DOTween.KillAll();
        DOTween.To(() => wheel.transform.eulerAngles.z % 360,
        value => wheel.transform.eulerAngles = new Vector3(0, 0, value),
        endValue, endValue / 360)
        .SetEase(Ease.OutSine)
        .OnComplete(() =>
        {
            luckyWheelCtrls[itemIndex].Select();
            onAfterStop?.Invoke(luckyWheelCtrls[itemIndex]);
        });
    }

    int RandomIndexItem()
    {
        float randomValue = UnityEngine.Random.Range(0f, 1f);
        float cumulativeProbability = 0;
        for (int i = 0; i < luckyWheelCtrls.Length; i++)
        {
            cumulativeProbability += luckyWheelCtrls[i].data.percent;
            if (randomValue <= cumulativeProbability)
                return i;
        }
        return -1;
    }
}
