
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Goods", menuName = "Goods")]
public class Goods : ScriptableObject
{
    public string goodsName;
    public int priceNow;
    public int priceOrigin;
    public Sprite icon;
    private float possibilityOfBreakdown = 0;
}
