using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class KnightController : MonoBehaviour
{
    public KnightHandler handler;
    public ActivePlayer player;

    public KnightControls knightControls;
    public bool active = true;

    [SerializeField] private GameObject HorsePrefab;
    [SerializeField] private float magnitude;
    [SerializeField] private float rotationSpeed;
    [SerializeField] public List<KnightControls> SavedControls;
    public int ControlsStepCount = 0;
    public Vector2 spawnLocation;
    public Horse horseScript;
    private GameObject horseObject;

    public void Start()
    {
        player = GetComponentInParent<ActivePlayer>();
        spawnLocation = transform.position;
        handler.resetHorses.AddListener(ResetHorse);
        ResetHorse();
        active = true;
    }

    public void FixedUpdate()
    {
        if (!horseScript)
        {
            player.RemoveKnight(this);
            this.enabled = false;
            return;
        }

        if (active)
        {
            SavedControls.Add(knightControls.CloneViaSerialization());
        }
        else if (ControlsStepCount < SavedControls.Count)
        {
            knightControls = SavedControls[ControlsStepCount];
            ControlsStepCount++;
        }
        else
        {
            knightControls = new();
            knightControls.relative = true;
            knightControls.up = true;
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


            horseScript.horseHead.AddRelativeForce(direction);
            horseScript.horseHead.AddRelativeForce(speed);
        }
    }

    public void ResetHorse()
    {
        enabled = true;
        Destroy(horseObject);
        active = (SavedControls.Count == 0);
        GameObject newHorse = Instantiate(HorsePrefab, transform);

        horseScript = newHorse.GetComponent<Horse>();

        horseObject = newHorse;
        ControlsStepCount = 0;
        horseScript.active = active;
        player.AddKnight(this);
    }
}