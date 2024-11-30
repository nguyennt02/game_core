using System;
using DG.Tweening;
using UnityEngine;

public class LuckWheelSystem : MonoBehaviour
{
    [SerializeField] LuckyWheelCtrl itemRewardPrefab;
    [SerializeField] Transform wheel;
    public bool spin = true;
    public bool stop = true;
    [Range(1, 5)]
    [SerializeField] int spinAmount;
    [Range(1, 5)]
    [SerializeField] float duration;
    float startAngles;
    int itemIndex = 0;
    [SerializeField] RewardData[] rewardDatas;
    LuckyWheelCtrl[] luckyWheelCtrls;
    public Action<RewardData> onAfterStop;
    public Action<bool> onBeforeSpin;
    public Action<bool> onBeforeStop;
    void Start()
    {
        SetUpItem();
        Spin();
    }
    public void SetUpItem()
    {
        luckyWheelCtrls = new LuckyWheelCtrl[rewardDatas.Length];
        startAngles = wheel.transform.eulerAngles.z;
        var eulerAngles = startAngles;
        var step = 360 / rewardDatas.Length;

        for (int i = 0; i < rewardDatas.Length;i++)
        {
            var luckyWheelCtrl = Instantiate(itemRewardPrefab,wheel);
            luckyWheelCtrl.InjectData(rewardDatas[i]);
            luckyWheelCtrl.SetUp();
            luckyWheelCtrl.NonSelect();
            luckyWheelCtrl.transform.rotation = Quaternion.AngleAxis(eulerAngles,Vector3.forward);
            eulerAngles += step;

            luckyWheelCtrls[i] = luckyWheelCtrl;
        }
    }

    public void Spin()
    {
        onBeforeSpin?.Invoke(spin);
        if (!spin) return;
        luckyWheelCtrls[itemIndex].NonSelect();
        var startValue = wheel.transform.eulerAngles.z;
        var endValue = startValue + 360;
        DOTween.To(() => startAngles,
        value => wheel.transform.rotation = Quaternion.AngleAxis(value,Vector3.forward),
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
        var startValue = wheel.transform.eulerAngles.z;
        var endValue = 360 - eulerAngles;
        if (startValue > endValue) endValue += 360;
        endValue += 360*spinAmount;

        DOTween.KillAll();
        DOTween.To(() => startValue,
        value => wheel.transform.rotation = Quaternion.AngleAxis(value,Vector3.forward),
        endValue, duration * endValue/360)
        .SetEase(Ease.OutSine)
        .OnComplete(() =>
        {
            luckyWheelCtrls[itemIndex].Select();
            onAfterStop?.Invoke(rewardDatas[itemIndex]);
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
