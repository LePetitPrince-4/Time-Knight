using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ActivePlayer : MonoBehaviour
{

    public int lostHorses;
    public int score;
    public Handler handler;
    public List<KnightController> knights;

    public PlayerControls playerControls = new();
    public KnightController knightController;

    public GameObject knightPrefab;
    public List<Vector2> SpawnPoints;
    public Color horseColour;
    public Color SwordColour;
    public Color RiderColour;
    public Color NPCRiderColour;
    public Charge selectedCharge;
    public FlagPole flagPole;

    public Vector2 directionOfCleanUp;
    public List<Rigidbody2D> rigidbody2CleanUp;

    public PlayModeSelectScreen testScreen;
    public Rigidbody2D activeHorseMiddle;
    private void Start()
    {  
        TranferPlayModeToKnight();
        
        flagPole.SetColor(RiderColour,SwordColour);
    }


    public void TranferPlayModeToKnight()
    {
        knightController.knightControls.relative = playerControls.relative;
        knightController.knightControls.compassBased = playerControls.compassBased;

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
        rigidbody2CleanUp.Clear();

        GameObject newKnight =  Instantiate(knightPrefab,transform);
        newKnight.transform.localPosition = SpawnPoints[0];
        SpawnPoints.RemoveAt(0);
        knightController = newKnight.GetComponent<KnightController>();
        knightController.handler = handler;
        knightController.active = true;
        knightController.knightControls.relative = playerControls.relative;
        knightController.knightControls.compassBased = playerControls.compassBased;


    }


    public void ReturnToTest()
    {
        if (testScreen)
        {
            knightController.ResetHorse(true);
            testScreen.ReturnFromTest();
        }

    }

    public void RemoveAllKnights()
    {
        
        foreach (KnightController knight in knights)
        {
            Destroy(knight?.horseScript);
        }
        
        knights.Clear();
        handler.EndRound(activeHorseMiddle);


    }

    public void KnightHit()
    {
        lostHorses++;
        handler.ScoreUpdate(this);
    }

    public void AddKnight(KnightController knight)
    {
        if (!knights.Contains(knight))
        {
            knights.Add(knight);
        }
    }


    public void HandleReset()
    {

        foreach (KnightController knight in knights)
        {
            if (knight.horseScript)
            {
                knight.horseScript.swordHandle.Fix();
                handler.reset.upgradingKnights.Add(knight);
                knight.horseScript.RebuildArmour();
            }
        }

        foreach (Rigidbody2D rigidbody in  rigidbody2CleanUp)
        {
            if (rigidbody)
            {
                Destroy(rigidbody.gameObject);

            }
        }
        rigidbody2CleanUp.Clear();
        
    }
}
