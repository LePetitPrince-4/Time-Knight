using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActivePlaneController : PlaneController
{
    public PlayerControls playerControls = new();
    private float planeLift;
    private float planeThrust;



    void Update()
    {        
        planeControls.faster = Input.GetKey(playerControls.faster);

        planeControls.fire = Input.GetKey(playerControls.fire);

        planeControls.up = Input.GetKey(playerControls.up);

        planeControls.down = Input.GetKey(playerControls.down);

        planeControls.fire = Input.GetKey(playerControls.fire);
        

         



    }
}
