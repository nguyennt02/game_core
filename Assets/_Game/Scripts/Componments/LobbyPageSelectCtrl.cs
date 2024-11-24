using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPageSelectCtrl : MonoBehaviour
{
    [Header("Background Button")]
    [SerializeField] Image bgrBtnImg;
    [SerializeField] Sprite bgrSelect;
    [SerializeField] Sprite bgrUncheck;

    [Space(10)]
    [Header("Icon")]
    [SerializeField] Image iconImg;
    [SerializeField] Sprite iconSelect;
    [SerializeField] Sprite iconUncheck;

    [Space(10)]
    [Header("Value anim")]
    [SerializeField] float toValue;
    [SerializeField] float formValue;
    [SerializeField] float duration;

    void Select()
    {
        // Set giao dien
        bgrBtnImg.color = Color.blue;
        iconImg.color = Color.red;
    }

    void Uncheck()
    {
        // Set giao dien
        bgrBtnImg.color = Color.yellow;
        iconImg.color = Color.green;
    }

    public void Selecting()
    {
        if (iconImg.TryGetComponent(out RectTransform rect))
        {
            Select();
            // chay anim
            DOTween.To(
                () => rect.anchoredPosition.y,
                (value) =>
                {
                    var pos = rect.anchoredPosition;
                    pos.y = value;
                    rect.anchoredPosition = pos;
                },
                toValue,
                duration);
        }
    }

    public void Unchecking()
    {
        if (iconImg.TryGetComponent(out RectTransform rect))
        {
            Uncheck();
            // chay anim
            DOTween.To(
                () => rect.anchoredPosition.y,
                (value) =>
                {
                    var pos = rect.anchoredPosition;
                    pos.y = value;
                    rect.anchoredPosition = pos;
                },
                formValue,
                duration);
        }
    }
}
