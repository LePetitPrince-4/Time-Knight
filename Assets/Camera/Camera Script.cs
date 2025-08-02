using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraScript : MonoBehaviour
{
    [SerializeField]private List<Transform> activePlanes;
    private float targetPosition;

    // Update is called once per frame
    void Update()
    {
        if (activePlanes.Count == 2)
        {

            Vector2 distanceBetweenPlanes = activePlanes[0].transform.position - activePlanes[1].transform.position;
            float aspectRatio = Camera.main.aspect;

            distanceBetweenPlanes.x = Mathf.Abs(distanceBetweenPlanes.x);
            distanceBetweenPlanes.y = Mathf.Abs(distanceBetweenPlanes.y);

            float cameraSize = Mathf.Max(10, distanceBetweenPlanes.x / aspectRatio, distanceBetweenPlanes.y);


            Camera.main.orthographicSize = cameraSize;
            Vector3 position = Vector2.Lerp(activePlanes[0].transform.position, activePlanes[1].transform.position, 0.5f);

            position.z = -10;



            //position = Vector3.Lerp(gameObject.transform.position, position, Time.unscaledDeltaTime);

            position.y = Mathf.Max(cameraSize, position.y);


            gameObject.transform.position = position;
        }else if (activePlanes.Count == 1)
        {
            Camera.main.orthographicSize = 10;
            Vector3 position = activePlanes[0].position;
            position.z = -10;


            gameObject.transform.position = position;


        }


    }

    public void RemovePlane(Transform transform)
    {
        if (activePlanes.Contains(transform))
        {
            activePlanes.Remove(transform);
        }
    }
}
