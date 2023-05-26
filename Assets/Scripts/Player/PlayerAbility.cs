using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbility : MonoBehaviour
{
    [Header("Points")]
    [SerializeField] private SOInt _assassinPoints;
    [SerializeField] private SOInt _stealthPoints;

    [Header("Abilities")]
    [SerializeField] private List<Ability> _unlockedAbilities;

    private void Start()
    {

    }
}
