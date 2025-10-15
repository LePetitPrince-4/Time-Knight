using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsHandler : MonoBehaviour
{
    public PlayerControls player1Controls = new();
    public PlayerControls player2Controls = new();

    public ColourSelectScreen player1Colours;
    public ColourSelectScreen player2Colours;

    public PlayModeSelectScreen player1ModeSelectScreen;
    public PlayModeSelectScreen player2ModeSelectScreen;
    public GameObject middleRow; 
    
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
        player1ModeSelectScreen.playerControls = player1Controls;
        player2ModeSelectScreen.playerControls = player2Controls;
    }

    public void HandleColourSelectScreen(ColourSelectScreen screen)
    {
        if (screen == player1Colours)
        {
            TransferToPlayer(screen, player1, player1ModeSelectScreen);
            player1ModeSelectScreen.gameObject.SetActive(true);
        }
        else if (screen = player2Colours)
        {
            TransferToPlayer(screen, player2, player2ModeSelectScreen);

        }

        middleRow.SetActive(true);



    }

    public void HandlePlayModeScreen()
    {
        if (FirstConfirm)
        {
            GameScreen.SetActive(true);
            middleRow.SetActive(false);
        }
        else
        {
            FirstConfirm = true;
        }

    }

    private void TransferToPlayer(ColourSelectScreen screen, ActivePlayer player, PlayModeSelectScreen modeSelectScreen)
    {
        player.SwordColour = screen.colourSelectOptions[0].GetColour().colour;
        player.RiderColour = screen.colourSelectOptions[1].GetColour().colour;
        player.horseColour = screen.colourSelectOptions[0].GetColour().secondaryColour;
        player.NPCRiderColour = screen.colourSelectOptions[1].GetColour().secondaryColour;
        modeSelectScreen.metal = screen.colourSelectOptions[0].GetColour();

        modeSelectScreen.tincture = screen.colourSelectOptions[1].GetColour();
        modeSelectScreen.gameObject.SetActive(true);
        
    }

    
}
