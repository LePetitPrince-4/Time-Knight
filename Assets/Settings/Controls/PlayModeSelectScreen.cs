using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayModeSelectScreen : MonoBehaviour
{
    public SettingsHandler settings;

    public PlayerControls playerControls;
    [SerializeField] private RectTransform arrowScreen;
    [SerializeField] private List<Image> arrows;
    [SerializeField] private List<TMP_Text> tmpTexts;

    [SerializeField] private List<Image> metalColoured;
    [SerializeField] private List<Image> tinctureColoured;

    [SerializeField] private List<Image> otherOptions;

    [SerializeField] private Vector3 relativeAngle;
    
    public Colours metal;
    public Colours tincture;
    public int highlightedOption;

    public ActivePlayer testPlayer;

    public void Start()
    {
        foreach (TMP_Text text in  tmpTexts)
        {
            text.color = metal.colour;
        }
        foreach (Image metalImage in metalColoured)
        {
            metalImage.color = metal.colour;
        }
        foreach (Image tinctureImage in tinctureColoured)
        {
            tinctureImage.color = tincture.colour;
        }

        ChangeHighlight(0);
    }
    
    public void Update()
    {
        if (Input.GetKeyDown(playerControls.down))
        {
            ChangeHighlight(1);
        }else if (Input.GetKeyDown(playerControls.up))
        {
            ChangeHighlight(-1);
        }else if (Input.GetKeyDown(playerControls.left))
        {
            InteractWith(-1);
        }else if (Input.GetKeyDown(playerControls.right))
        {
            InteractWith(1);

        }
    }


    public void Test()
    {
        testPlayer.playerControls = playerControls;
        testPlayer.TranferPlayModeToKnight(); 
        testPlayer.SwordColour = metal.colour;
        testPlayer.horseColour = metal.secondaryColour;
        testPlayer.RiderColour = tincture.colour;
        
        testPlayer.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ReturnFromTest()
    {
        testPlayer.gameObject.SetActive(false);
        gameObject.SetActive(true);

    }
    
    

    public void ChangeHighlight(int direction)
    {
        highlightedOption += direction;

        highlightedOption = Math.Clamp(highlightedOption, 0, 2);

        foreach (Image option in otherOptions)
        {
            option.enabled = false;
        }
        
        if (highlightedOption == 0)
        {
            HighlightArrows(true);
        }else
        {
            otherOptions[highlightedOption - 1].enabled = true;
            HighlightArrows(false);
        }
    }


    public void InteractWith(int direction)
    {
        if (highlightedOption == 0)// horsey
        {
            ChangeControls();
        }else if (highlightedOption == 1)
        {
            Test();
        }
        else
        {
            testPlayer.gameObject.SetActive(false);

            settings.HandlePlayModeScreen();
            gameObject.SetActive(false);
        }
    }

    public void HighlightArrows(bool highlight)
    {
        Color arrowColour = Color.clear;
        if (highlight)
        {
            arrowColour = tincture.colour;

        }
        else
        {
            arrowColour = metal.colour;

        }
        foreach (Image arrow in arrows)
        {
            arrow.color = arrowColour;
        }
    }

    public void ChangeControls()
    {
        playerControls.relative = !playerControls.relative;
        playerControls.compassBased = !playerControls.compassBased;
        SetArrows();

    }

    public void SetArrows()
    {
        if (playerControls.compassBased)
        {
            arrowScreen.localEulerAngles = Vector3.zero;
        }
        else if (playerControls.relative)
        {
            arrowScreen.localEulerAngles = relativeAngle;
        }
    }
    
}
