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

    public KeyCode faster;
    public KeyCode fire;
}
[Serializable]

public class PlaneControls
{
    public bool up;
    public bool down;

    public bool faster;
    public bool fire;
}


