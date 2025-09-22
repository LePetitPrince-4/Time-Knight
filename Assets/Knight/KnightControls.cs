using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

[Serializable]
public class PlayerControls 
{
    public KeyCode up;
    public KeyCode down;

    public KeyCode left;
    public KeyCode right;
}
[Serializable]

public class KnightControls
{
    public bool up;
    public bool down;

    public bool left;
    public bool right;

    public bool compassBased;
    public bool relative;
}


