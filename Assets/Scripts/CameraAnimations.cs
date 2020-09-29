using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimations : MonoBehaviour
{
    
    [SerializeField] float zoomInSpeed = 1f;
    [SerializeField] float zoomOutSpeed = 1f;
    [SerializeField] float targetZoomIn = 0.3f;
    float initialZoom = 0f;
    float initialPosX;
    float initialPosY;
    public static bool IsZoomedIn = false;
    public static bool IsZoomedOut = true;

    [SerializeField] Transform ball;
    Camera cam;


    private void Start()
    {
        cam = Camera.main;
        initialZoom = cam.orthographicSize;
        initialPosX = cam.transform.position.x;
        initialPosY = cam.transform.position.y;
    }


    public void ZoomInCamera()
    {
        if (!IsZoomedIn)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoomIn, Time.deltaTime * zoomInSpeed);
            cam.transform.position = new Vector3(Mathf.Lerp(cam.transform.position.x, ball.position.x, Time.deltaTime * zoomInSpeed),
                Mathf.Lerp(cam.transform.position.y, ball.position.y, Time.deltaTime * zoomInSpeed),
                cam.transform.position.z);
            if (cam.orthographicSize - targetZoomIn <= .001f)
            {
                IsZoomedIn = true;
            }
        }
    }

    public void ZoomOutCamera()
    {
        if (IsZoomedIn)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, initialZoom, Time.deltaTime * zoomOutSpeed);
            cam.transform.position = new Vector3(Mathf.Lerp(cam.transform.position.x, initialPosX, Time.deltaTime * zoomOutSpeed),
                Mathf.Lerp(cam.transform.position.y, initialPosY, Time.deltaTime * zoomOutSpeed),
                cam.transform.position.z);
            if (initialZoom - cam.orthographicSize <= .05f)
            {
                IsZoomedIn = false;
                IsZoomedOut = true;
                cam.orthographicSize = initialZoom;
                cam.transform.position = new Vector3(initialPosX, initialPosY, cam.transform.position.z);
                GameStatus.InTransition = false;
            }
        }
    }

}
