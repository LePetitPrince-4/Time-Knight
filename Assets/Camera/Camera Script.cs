using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraScript : MonoBehaviour
{
    [SerializeField]private List<Transform> activeKnight;
    private float targetPosition;

    // Update is called once per frame
    void Update()
    {
        if (activeKnight.Count == 2)
        {

            Vector2 distanceBetweenPlanes = activeKnight[0].transform.position - activeKnight[1].transform.position;
            float aspectRatio = Camera.main.aspect;

            distanceBetweenPlanes.x = Mathf.Abs(distanceBetweenPlanes.x);
            distanceBetweenPlanes.y = Mathf.Abs(distanceBetweenPlanes.y);

            float cameraSize = Mathf.Max(10, distanceBetweenPlanes.x / aspectRatio, distanceBetweenPlanes.y);


            Camera.main.orthographicSize = cameraSize;
            Vector3 position = Vector2.Lerp(activeKnight[0].transform.position, activeKnight[1].transform.position, 0.5f);

            position.z = -10;



            //position = Vector3.Lerp(gameObject.transform.position, position, Time.unscaledDeltaTime);



            gameObject.transform.position = position;
        }else if (activeKnight.Count == 1)
        {
            Camera.main.orthographicSize = 10;
            Vector3 position = activeKnight[0].position;
            position.z = -10;


            gameObject.transform.position = position;


        }


    }

    public void RemoveKnight(Transform transform)
    {
        if (activeKnight.Contains(transform))
        {
            activeKnight.Remove(transform);
        }
    }
}
