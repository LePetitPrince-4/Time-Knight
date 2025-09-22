using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActiveKnightController : MonoBehaviour
{
    public PlayerControls playerControls = new();
    public KnightController knightController;

    private void Start()
    {
        knightController.knightControls.relative = true;
    }

    void Update()
    {        
        knightController.knightControls.right = Input.GetKey(playerControls.right);

        knightController.knightControls.left = Input.GetKey(playerControls.left);
        
        knightController.knightControls.up = Input.GetKey(playerControls.up);

        knightController.knightControls.down = Input.GetKey(playerControls.down);
    }
}
