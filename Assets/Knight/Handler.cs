using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class Handler : MonoBehaviour
{
    public RoundReset reset;
    public UnityEvent resetHorses;
    public EdgeCollider2D blocker;
    public List<ActivePlayer> players;
    public GameObject horseShoe;
    public DeathBarrier barrier;
    public bool resetRecently;
    public int rigidBodyCount;
    public bool stillPlaying = true;
    public int roundCount;
    public float acceleration = 1;
    public void NewDay()
    {
        roundCount++;

        acceleration = 10;
        if (resetRecently)
        {
            return;
        }

        stillPlaying = true;
        barrier.blocking = true;

        resetRecently = true;
        resetHorses.Invoke();
        foreach (ActivePlayer player in players)
        {
            player.NewKnight();
        }

        
        Invoke("ReadyForNextTurn", 2);
    }

    public void EndRound(Rigidbody2D deadKnight)
    {
        barrier.blocking = false;
        reset.ResetRound(deadKnight);
    }

    public void ReadyForNextTurn()
    {
        resetRecently = false;
    }

    public void ScoreUpdate(ActivePlayer player)
    {
        int score = player.lostHorses;
        foreach (ActivePlayer activePlayer in players)
        {
            if (activePlayer != player)
            {
                activePlayer.flagPole.SetScore(score);
            }
        }
    }
    
    public void Update()
    {
        if (Input.GetKey("r"))
        {
            SceneManager.LoadScene("Scenes/The Field");
        }
    }




}
