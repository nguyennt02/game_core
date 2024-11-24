using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BaseUIRoot : MonoBehaviour
{
    [SerializeField] Transform modal;
    public Action onBeforeShowModal;
    public Action onAfterHideModal;
    public BaseUIRoot ShowModal()
    {
        onBeforeShowModal?.Invoke();
        onBeforeShowModal = null;

        gameObject.SetActive(true);
        modal.localScale = Vector3.zero;
        modal.DOScale(1, 0.3f);
        return this;
    }
    public BaseUIRoot HideModal()
    {
        modal.localScale = Vector3.one;
        modal.DOScale(0, 0.3f).OnComplete(() =>
        {
            onAfterHideModal?.Invoke();
            onAfterHideModal = null;
            
            gameObject.SetActive(false);
        });
        return this;
    }
}
