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


public class KnightHandler : MonoBehaviour
{

    public UnityEvent resetHorses;
    public EdgeCollider2D blocker;
    public List<ActivePlayer> players;

    public DeathBarrier barrier;
    public bool resetRecently;

    public int rigidBodyCount;
    public bool stillPlaying = true;

    public float acceleration = 1;
    public void NewDay()
    {
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

    public void EndRound()
    {
        barrier.blocking = false;
        stillPlaying = false;
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

    public void FixedUpdate()
    {
        if (!stillPlaying)
        {
            acceleration += 1;
        }
    }
}
