using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColourSelectOption : MonoBehaviour
{

    [SerializeField] private List<Colours> coloursList;
    [SerializeField] private TMP_Text colourText;
    [SerializeField] private Image highlightAura;
    [SerializeField] private List<Image> colourShowcase;
    public int currentColour = 0;

    [SerializeField] private ColourSelectOption pairedColourSelect;
    private bool highlighted;
    public void Start()
    {
        SetColour();
    }

    public void Highlight(bool activateHighlight)
    {
        highlightAura.enabled = activateHighlight;
        highlighted = activateHighlight;
    }

    public void ChangeColour(int direction)
    {
        if (highlighted)
        {
            
            Debug.Log("ChangeColour");
                
            adjustColour(direction);
            if (pairedColourSelect.currentColour == currentColour)
            {
                adjustColour(direction);
            }
            
            SetColour();
        }
    }

    private void adjustColour(int direction)
    {
        currentColour += direction;

        while (currentColour < 0)
        {
            currentColour += coloursList.Count;
        }

        currentColour = currentColour % coloursList.Count;
    }
    
    

    private void SetColour()
    {
        Colours activeColour = coloursList[currentColour];

        if (activeColour.isMetal)
        {
            colourText.color = Color.black;

        }else
        {
            colourText.color = Color.white;

        }
        colourText.text = activeColour.name;

        foreach (Image showcase in colourShowcase)
        {
            showcase.color = activeColour.colour;

        }
        

    }

    public Colours GetColour()
    {
        Colours activeColour = coloursList[currentColour];

        return activeColour;
    }
    
    
    
    

}
