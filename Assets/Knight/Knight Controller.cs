using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class KnightController : MonoBehaviour
{
    public KnightControls knightControls;
    private bool active = true;
    [SerializeField] private Rigidbody2D horseHead;
    [SerializeField] private Rigidbody2D Sword;
    
    [SerializeField] private float magnitude;    
    [SerializeField] private float rotationSpeed;    
    [SerializeField] public List<KnightControls> SavedControls;
    public int ControlsStepCount = 0;
    public Vector2 spawnLocation;
    public Horse horse;
    public void Start()
    {
        spawnLocation = transform.position;
    }
    public void FixedUpdate()
    {
        if (!horse)
        {
            return;
        }
        if (active)
        {
            SavedControls.Add(knightControls);
        }
        else
        {
            knightControls = SavedControls[ControlsStepCount];
            ControlsStepCount++;
        }

        if (knightControls.relative)
        {
            Vector2 speed = Vector2.up;
            Vector2 direction = Vector2.zero;
            if (knightControls.up)
            {
                speed += Vector2.up;
            }

            if (knightControls.down)
            {
                speed += Vector2.down;
            }
        
        
            if (knightControls.left)
            {
                direction += Vector2.left;
            }
            if (knightControls.right)
            {
                direction += Vector2.right;
            }
            
    
            speed *= magnitude;
            direction *= rotationSpeed;
        

            horseHead.AddRelativeForce(direction);
            horseHead.AddRelativeForce(speed);
        }
    }

}