using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class KnightHandler : MonoBehaviour
{

    public UnityEvent resetHorses;

    public List<ActivePlayer> players;


    public void NewDay()
    {
        resetHorses.Invoke();
        foreach (ActivePlayer player in players)
        {
            player.NewKnight();
        }
        
    }


}
