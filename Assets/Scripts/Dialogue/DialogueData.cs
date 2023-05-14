using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/New Dialogue Data")]
public class DialogueData : ScriptableObject
{
    public string speakerName;
    public Sprite speakerSprite;
    public List<string> _texts;
}
