using UnityEngine;
[CreateAssetMenu(fileName = "NewItem", menuName = "Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public int value;
    public GameObject prefab;
    public int minDropCount;
    public int maxDropCount;
}
