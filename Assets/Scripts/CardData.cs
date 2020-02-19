using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Card Data", menuName = "Scriptable Object/Card Data", order = int.MaxValue)]
public class CardData : ScriptableObject
{
    [SerializeField]
    private int cost;
    public int Cost { get { return cost; } }

    [SerializeField]
    private Image cardImage;
    public Image CardImage { get { return cardImage; } }
}
