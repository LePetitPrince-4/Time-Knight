using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsHandler : MonoBehaviour
{
    public PlayerControls player1Controls = new();
    public PlayerControls player2Controls = new();

    public ColourSelectScreen player1Colours;
    public ColourSelectScreen player2Colours;

    public ActivePlayer player1;
    public ActivePlayer player2;
    public GameObject GameScreen;

    public bool FirstConfirm = false;

    public void Awake()
    {
        TransferControls();
    }

    public void TransferControls()
    {
        player1Colours.playerControls = player1Controls;
        player2Colours.playerControls = player2Controls;
        player1.playerControls = player1Controls;
        player2.playerControls = player2Controls;

    }

    public void HandleColourSelectScreen(ColourSelectScreen screen)
    {
        if (screen == player1Colours)
        {
            TransferToPlayer(screen, player1);
        }
        else if (screen = player2Colours)
        {
            TransferToPlayer(screen, player2);
        }

        if (FirstConfirm)
        {
            GameScreen.SetActive(true);
        }
        else
        {
            FirstConfirm = true;
        }
    }

    private void TransferToPlayer(ColourSelectScreen screen, ActivePlayer player)
    {
        player.SwordColour = screen.colourSelectOptions[0].GetColour();
        player.RiderColour = screen.colourSelectOptions[1].GetColour();
        player.horseColour = screen.colourSelectOptions[0].GetSecondaryColour();
        player.NPCRiderColour = screen.colourSelectOptions[1].GetSecondaryColour();

    }
    
}
