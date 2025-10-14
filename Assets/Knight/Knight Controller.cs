using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KnightController : MonoBehaviour
{
    public KnightHandler handler;
    public ActivePlayer player;

    public KnightControls knightControls;
    public bool active = true;

    [SerializeField] private GameObject horsePrefab;
    [SerializeField] private float magnitude;

    [SerializeField] private List<float> rotation;
    public Vector2 velocity;
    [SerializeField] private List<Vector2> forces;
    
    public int controlsStepCount = 0;
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
            if (active)
            {
                player.RemoveAllKnights();
            }
            if (this.enabled)
            {
                player.rigidbody2CleanUp.AddRange(GetComponentsInChildren<Rigidbody2D>().ToList());
                player.KnightHit();
            }
            this.enabled = false;
            return;
        }
        if (active)
        {
            
            
            horseScript.horseHead.AddRelativeForce(Vector2.up * magnitude/2);

            Vector2 speed = Vector2.zero;

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
                speed += Vector2.left;
            }

            if (knightControls.right)
            {
                speed += Vector2.right;
            }

            speed = speed.normalized;

            speed = speed * magnitude;

            if (knightControls.relative)
            {
                horseScript.horseHead.AddRelativeForce(speed);
            }
            else if (knightControls.compassBased)
            {
                horseScript.horseHead.AddForce(speed);
            }

        }




        if (active)
        {
            forces.Add(horseScript.horseHead.totalForce);
            rotation.Add(horseScript.horseHead.rotation);
        }
        else if (controlsStepCount < forces.Count)
        {
            horseScript.horseHead.AddForce(forces[controlsStepCount]);
            horseScript.horseHead.rotation = rotation[controlsStepCount];
            forces[controlsStepCount] = horseScript.horseHead.totalForce;
            controlsStepCount++;
        }
        else
        {
            horseScript.DeclareOffScript();
            controlsStepCount = 0;
        }

    }

    public void ResetHorse()
    {
        enabled = true;
        Destroy(horseObject);
        active = (forces.Count == 0);
        GameObject newHorse = Instantiate(horsePrefab, transform);

        horseScript = newHorse.GetComponent<Horse>();
        horseScript.handler = handler;
        horseObject = newHorse;
        controlsStepCount = 0;
        horseScript.active = active;
        player.AddKnight(this);
    }


}