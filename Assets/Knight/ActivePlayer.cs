using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ActivePlayer : MonoBehaviour
{
    public KnightHandler handler;
    public List<KnightController> knights;

    public PlayerControls playerControls = new();
    public KnightController knightController;

    public GameObject knightPrefab;
    public List<Vector2> SpawnPoints;
    public Color horseColour;
    public Color SwordColour;
    public Color RiderColour;

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
    
    public void NewKnight()
    {
        GameObject newKnight =  Instantiate(knightPrefab,transform);
        newKnight.transform.localPosition = SpawnPoints[0];
        SpawnPoints.RemoveAt(0);
        knightController = newKnight.GetComponent<KnightController>();
        knightController.handler = handler;
        knightController.active = true;
        knightController.knightControls.relative = playerControls.relative;
        knightController.knightControls.compassBased = playerControls.compassBased;


    }
    public void RemoveKnight(KnightController knight)
    {
        knights.Remove(knight);
        if (knights.Count == 0)
        {
            handler.Invoke("NewDay", 1);
        }
    }

    public void AddKnight(KnightController knight)
    {
        if (!knights.Contains(knight))
        {
            knights.Add(knight);
        }
    }

    
    
}
