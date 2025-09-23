using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class KnightHandler : MonoBehaviour
{

    public UnityEvent resetHorses;

    public List<ActivePlayer> players;
    public bool resetRecently;

    public void NewDay()
    {
        if (resetRecently)
        {
            return;
        }

        resetRecently = true;
        resetHorses.Invoke();
        foreach (ActivePlayer player in players)
        {
            player.NewKnight();
        }

        Invoke("ReadyForNextTurn", 2);
    }

    public void ReadyForNextTurn()
    {
        resetRecently = false;
    }

    public void Update()
    {
        if (Input.GetKey("r"))
        {
            SceneManager.LoadScene("Scenes/The Field");
        }
    }
}
