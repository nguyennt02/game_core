using UnityEngine;

[CreateAssetMenu(fileName = "RewardData", menuName = "Data", order = 1)]
public class RewardData : ScriptableObject
{
    public TypeItem typeItem;
    public Sprite icon;
    public int amount;
    [Range(0, 1)]
    public float percent;
}