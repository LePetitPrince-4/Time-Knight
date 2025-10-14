using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColourSelectScreen : MonoBehaviour
{
    public SettingsHandler settings;
    public PlayerControls playerControls = new();
    public List<ColourSelectOption> colourSelectOptions;
    public int highlightedPlayerControls;
    public Image confirmHighlight; 
    public void Start()
    {
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
            ChangeColour(-1);
        }else if (Input.GetKeyDown(playerControls.right))
        {
            ChangeColour(1);
        }
    }

    private void ChangeHighlight(int direction)
    {
        highlightedPlayerControls += direction;

        highlightedPlayerControls = Math.Clamp(highlightedPlayerControls, 0, colourSelectOptions.Count);
        for (int i = 0; i < colourSelectOptions.Count; i++)
        {
            ColourSelectOption selectOption = colourSelectOptions[i];
            
            if (i == highlightedPlayerControls)
            {
                selectOption.Highlight(true);

            }
            else
            {
                selectOption.Highlight(false);

            }
        }

        if (highlightedPlayerControls == colourSelectOptions.Count)
        {
            confirmHighlight.enabled = true;

        }
        else
        {
            confirmHighlight.enabled = false;
        }

    }

    private void ChangeColour(int direction)
    {
        if (highlightedPlayerControls < colourSelectOptions.Count)
        {
            colourSelectOptions[highlightedPlayerControls].ChangeColour(direction);
        }else if (direction > 0)
        {
            settings.HandleColourSelectScreen(this);
            gameObject.SetActive(false);
        }

    }
}
