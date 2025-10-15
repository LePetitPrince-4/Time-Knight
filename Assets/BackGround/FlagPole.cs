using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlagPole : MonoBehaviour
{
    public Vector2 startingPosition;
    public Vector2 endingPosition;
    private const int maxScore = 36;

    [SerializeField] private GameObject flag;
    [SerializeField] private SpriteRenderer pole;

    public void SetColor(Color flagColour, Color poleColour)
    {
        pole.color = poleColour;

        flag.GetComponent<SpriteRenderer>().color = flagColour;
    }
    
    public void SetScore(int score)
    {

        float height = (float)score / (float)maxScore;
        
        
        Vector2 position = Vector2.Lerp(startingPosition,endingPosition,height);

        flag.transform.localPosition = position;

    }
    
}
