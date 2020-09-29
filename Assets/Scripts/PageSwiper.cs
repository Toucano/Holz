using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 panelLocation;
    public float percentThreshold = 0.2f;
    public float easing = 0.5f;
    public int totalPages = 1;
    private int currentPage = 1;

    [SerializeField] CanvasGroup menuPage;
    [SerializeField] CanvasGroup progressionPage;
    [SerializeField] Camera cam;


    void Start()
    {
        cam = Camera.main;
        panelLocation = transform.position;
        Debug.Log("helo");
    }

    void Update()
    {
        menuPage.alpha = -1f/5f * cam.ScreenToWorldPoint(transform.position).y + 1f; //Don't touch these variables they're well calculated
        progressionPage.alpha = 1f/5f * cam.ScreenToWorldPoint(transform.position).y - 1f; //Don't touch these variables they're well calculated
    }
    public void OnDrag(PointerEventData data)
    {
        float difference = data.pressPosition.y - data.position.y;
        transform.position = panelLocation - new Vector3(0, difference, 0);
    }
    public void OnEndDrag(PointerEventData data)
    {
        float percentage = (data.pressPosition.y - data.position.y) / Screen.height;
        if (Mathf.Abs(percentage) >= percentThreshold)
        {
            Vector3 newLocation = panelLocation;
            if (percentage <= 0 && currentPage < totalPages)
            {
                Debug.Log(currentPage);
                currentPage++;
                newLocation += new Vector3(0, Screen.height, 0);
            }
            else if (percentage >= 0 && currentPage > 1)
            {
                currentPage--;
                newLocation += new Vector3(0, -Screen.height, 0);
            }
            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
            panelLocation = newLocation;

        }
        else
        {
            StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
        }
    }

    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            Debug.Log(transform.position);
            yield return null;
        }
    }

    //private float ChangingMenuAlpha(float cursor)
    //{
    //    var menuAlpha = Mathf.Lerp(0f, 1f, cursor);
    //    return menuAlpha;
    //}
    //private float ChangingProgressionAlpha(float cursor)
    //{
    //    var progAlpha = Mathf.Lerp(1f, 0f, cursor);
    //    return progAlpha;
    //}
}            


//swipeLeft.color = new Color(swipeLeft.color.r, swipeLeft.color.g, swipeLeft.color.b, Mathf.Lerp(swipeLeft.color.a, 0, transform.position.x * 0.01f));
//swipeRight.color = new Color(swipeLeft.color.r, swipeLeft.color.g, swipeLeft.color.b, Mathf.Lerp(swipeLeft.color.a, 0, transform.position.x * 0.01f));