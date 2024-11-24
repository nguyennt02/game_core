using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectSystem : MonoBehaviour
{
    [Header("Level setup")]
    [SerializeField] LevelSlectCtrl levelPrefab;
    [SerializeField] int limitSelect;
    [SerializeField] int amountLevel;
    void Awake()
    {
        SetUp(4);
    }
    void Start()
    {
        SelectLevel(4);

    }

    void SelectLevel(int currentLevel)
    {
        int index = currentLevel - 1;
        if (currentLevel > limitSelect) index = limitSelect - 1;
        if (TryGetComponent(out RectTransform rect)
            && transform.GetChild(0).TryGetComponent(out RectTransform rect1))
        {
            rect.anchoredPosition = new Vector2(0, -index * rect1.rect.height);
        }
    }

    void SetUp(int currentLevel)
    {
        int level = 0;
        if (currentLevel > limitSelect)
        {
            level = currentLevel - limitSelect;
        }
        for (int i = 0; i < amountLevel; i++)
        {
            var levelObj = Instantiate(levelPrefab, transform);
            levelObj.transform.SetAsFirstSibling();
            if (level < currentLevel)
            {
                levelObj.Select(++level);
            }
            else
            {
                levelObj.Unchecked(++level);
            }
        }
    }
}
