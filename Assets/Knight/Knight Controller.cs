using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;

public class KnightController : MonoBehaviour
{
    public Handler handler;
    public ActivePlayer player;

    public KnightControls knightControls;
    public bool active = true;
    public bool riderless = false;
    [SerializeField] private GameObject horsePrefab;
    [SerializeField] private float magnitude;

    [SerializeField] private List<float> rotation;
    public Vector2 velocity;
    [SerializeField] private List<Vector2> forces;

    public List<int> enchantedArmour;
    public int controlsStepCount = 0;
    public Vector2 spawnLocation;
    public Horse horseScript;
    private GameObject horseObject;
    public const int stepsPerHorseShoe = 15;
    public Color HorseShoeColour;
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
        int stepCount = 0;
        if (!horseScript)
        {
            if (active)
            {
                player.RemoveAllKnights();
            }
            else if (this.enabled)
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
            stepCount = forces.Count;
        }
        else if (controlsStepCount < forces.Count)
        {
            horseScript.horseHead.AddForce(forces[controlsStepCount]);
            horseScript.horseHead.rotation = rotation[controlsStepCount];
            stepCount = controlsStepCount;

            if (riderless)
            {
                stepCount += forces.Count;
            }
            controlsStepCount++;
        }
        else
        {
            horseScript.DeclareOffScript();
            controlsStepCount = 0;
            riderless = true;
        }

        if (active &&  stepCount % stepsPerHorseShoe == 0)
        {
            SpawnHorseShoe(stepCount);
        }

    }

    public void ResetHorse()
    {
        ResetHorse( false);
    }
    
    public void ResetHorse(bool setActive)
    {
        enabled = true;
        Destroy(horseObject);
        if (!active)
        {
            ClearHorseShoes();
        }
        else
        {
            ColourHorseShoes(player.SwordColour);
        }
        
        active = setActive || (forces.Count == 0);
        GameObject newHorse = Instantiate(horsePrefab, transform);

        horseScript = newHorse.GetComponent<Horse>();
        horseScript.handler = handler;
        horseObject = newHorse;
        controlsStepCount = 0;
        horseScript.active = active;
        player.AddKnight(this);
        riderless = false;
        foreach (int i in enchantedArmour)
        {
            Armour armour = horseScript.armours[i];
            armour.Enchant();
        }
    }
    public void HandleUpgrade()
    {
        List<Armour> validArmour = new();
        if (!horseScript)
        {
            return;
        }

        bool foundUpgrade = false;
        for (int i = 0; i < horseScript.armours.Count;i ++ )
        {
            Armour armour = horseScript.armours[i];
            if (armour.enchanted)
            {
                continue;
            }

            if (!foundUpgrade)
            {
                armour.Enchant();
                foundUpgrade = true;
                enchantedArmour.Add(i);
            }
        }
    }

    public void SpawnHorseShoe(int stepCount)
    {
        Transform spawnTransform;
        int HorseshoeIndex = stepCount / stepsPerHorseShoe;
        if (HorseshoeIndex % 2 == 0)
        {
            spawnTransform = horseScript.horseBack;
        }
        else
        {
            spawnTransform = horseScript.horseMiddle.transform;
        }
        
        GameObject horseShoe = Instantiate(handler.horseShoe, spawnTransform);
        horseShoe.transform.parent = gameObject.transform;
        
        horseShoe.GetComponent<SpriteRenderer>().sortingOrder = handler.roundCount;
    }

    public void ClearHorseShoes()
    {
        int count = 0;
        HorseShoeColour.a = HorseShoeColour.a - 0.75f;

        if (HorseShoeColour.a > 0f)
        {
            ColourHorseShoes(HorseShoeColour);
            return;
        }
        
        foreach (Transform child in transform)
        {
            if (child.gameObject.layer == 14)
            {
                Destroy(child.gameObject);                
            }
        }

    }

    private void ColourHorseShoes(Color colour)
    {
        HorseShoeColour = colour;
        
        foreach (Transform child in transform)
        {
            if (child.gameObject.layer == 14)
            {
                child.GetComponent<SpriteRenderer>().color = colour;
            }
        }

    }
    


}