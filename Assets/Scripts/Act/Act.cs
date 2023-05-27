using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = " New Act")]
public class Act : ScriptableObject
{
    public string Title;
    public string Description;
    public bool Seen;
}
