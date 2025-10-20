using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundReset : MonoBehaviour
{
    public Handler handler;
    public bool wait;
    public List<KnightController> upgradingKnights;

    public Camera MainCam;
    public CameraScript ZoomCamera;
    public const float minTimeScale = 0.1f;
    public void ResetRound(Rigidbody2D deadKnight)
    {
        StartCoroutine(HandleReset(deadKnight));
    }


    private IEnumerator HandleReset(Rigidbody2D startingCamTransform)
    {
        wait = false;
        float targetTime = 1;
        bool slowingDown = false;
        int knightCount = 0;
        MainCam.gameObject.SetActive(false);

        ZoomCamera.enabled = true;
        ZoomCamera.GoToTarget(startingCamTransform,0);
        while (targetTime > minTimeScale)
        {
            targetTime -= Time.unscaledDeltaTime;
            targetTime = Mathf.Max(minTimeScale, targetTime);
            Time.timeScale = targetTime;
            yield return new WaitForSecondsRealtime(0f);
        }   
        foreach (ActivePlayer player in handler.players)
        {
            player.HandleReset();
        }

        yield return new WaitForFixedUpdate();
        Time.timeScale = 0f;
        
        yield return new WaitForSecondsRealtime(1f);

        while (knightCount < upgradingKnights.Count)
        {
            ZoomCamera.GoToTarget(upgradingKnights[knightCount].horseScript.horseMiddle, 1f);

            yield return new WaitForSecondsRealtime(1f);
            
            upgradingKnights[knightCount].HandleUpgrade();
            knightCount++;
            yield return new WaitForSecondsRealtime(1f);


        }

        ZoomCamera.GoToTarget(startingCamTransform,1);

        
        while (targetTime < 1)
        {
            targetTime += Time.unscaledDeltaTime;
            targetTime = Mathf.Min(1, targetTime);
            Time.timeScale = targetTime;
            yield return new WaitForSecondsRealtime(0f);
        }
        MainCam.gameObject.SetActive(true);

        handler.NewDay();
        
        upgradingKnights.Clear();

        ZoomCamera.enabled = false;
    }

    
    
    
}
