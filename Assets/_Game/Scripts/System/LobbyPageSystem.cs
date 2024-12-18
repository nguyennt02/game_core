using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LobbyPageSystem : MonoBehaviour
{
    [SerializeField] GameObject[] pages;
    [SerializeField] float duration;
    float temp = 0;
    void Start()
    {
        LobbyPageSelectSystem.OnSelect += SelectAt;
        SelectAt(1);
    }
    void OnDestroy()
    {
        LobbyPageSelectSystem.OnSelect -= SelectAt;
    }

    void SelectAt(int index)
    {
        Camera mainCamera = Camera.main;
        Vector2 posCamera = mainCamera.transform.position;
        float witdhCamera = mainCamera.orthographicSize * 2f * mainCamera.aspect;
        Sequence seq = DOTween.Sequence();
        for (int i = 0; i < pages.Length; i++)
        {
            Vector2 pos = posCamera;
            if (i < index)
                pos.x = pos.x - witdhCamera * (index - i);
            else if (i > index)
                pos.x = pos.x + witdhCamera * (i - index);
            seq.Join(
                pages[i].transform.DOMove(pos, temp)
            );
        }
        seq.OnComplete(() => temp = duration);
    }
}
