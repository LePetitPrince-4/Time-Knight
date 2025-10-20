using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Charge", menuName = "Settings/Charge", order = 3)]

public class Charge : ScriptableObject
{
    public string Description;
    public List<Sprite> armourEnchantments;
}
