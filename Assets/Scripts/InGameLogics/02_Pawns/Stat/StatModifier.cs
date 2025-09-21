
public enum ModifierType
{
    Add,
    Multiply,
    Override
}

[System.Serializable]
public class StatModifier
{
    public EStatType Type;
    public float Value;
    public ModifierType ModType;

    public StatModifier(EStatType type, float value, ModifierType modType)
    {
        Type = type;
        Value = value;
        ModType = modType;
    }

    public override string ToString() => $"{Type} {ModType} : {Value}";

}