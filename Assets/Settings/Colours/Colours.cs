using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Colours", menuName = "Settings/Colour", order = 3)]

public class Colours : ScriptableObject
{
    public string name;
    public Color colour;
    public bool isMetal;
    public Color secondaryColour;
}
