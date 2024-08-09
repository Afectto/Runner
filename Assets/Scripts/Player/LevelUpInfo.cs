using UnityEngine;

[CreateAssetMenu(menuName = "Data/Level Up Info")]
public class LevelUpInfo : ScriptableObject
{
    [SerializeField] private float lvl;
    [SerializeField] private float valueToNextLvl;
    [SerializeField] private Color color;
    [SerializeField] private string textName;

    public float Lvl => lvl;
    public float ValueToNextLvl => valueToNextLvl;
    public Color Color => color;
    public string TextName => textName;
}
