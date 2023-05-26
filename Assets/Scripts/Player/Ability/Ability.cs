using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Ability : ScriptableObject
{
    public enum AbilityType
    {
        ASSASSIN,
        STEALTH
    }

    public string Title;
    public Sprite Icon;
    public string Description;
    public int Cost;
    public AbilityType Type;
}
