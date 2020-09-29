using System.Collections;
using UnityEngine;

public class DragNShoot : MonoBehaviour
{
    public float power = 10f;
    public Rigidbody2D rb;

    public Vector2 minPower;
    public Vector2 maxPower;

    TrajectoryLine Tl;

    Vector2 force;
    Vector3 startPoint;
    Vector3 endPoint;

    Camera cam;

    [SerializeField] bool HardMode;

    [SerializeField] bool isMouseTesting = false;
    private void Awake()
    {
        GameEvents.current.onBallInRightHole += StopBall;
        GameEvents.current.onBallInWrongHole += StopBall;
    }
    private void Start()
    {
        cam = Camera.main;
        Tl = GetComponent<TrajectoryLine>();

        Time.timeScale = 1f;//I added this line

    }
    
    private void Update()
    {
        if (!GameStatus.InTransition && !GameStatus.IsDead)
        {
            DragNShootControl();
        }
        if (GameStatus.InTransition && !GameStatus.IsDead)
        {
            Tl.EndLine();
        }
    }

    private void DragNShootControl()
    {
        Debug.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y) + rb.velocity, Color.red);
        if (!isMouseTesting)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 currentPoint = cam.ScreenToWorldPoint(touch.position);
                currentPoint.z = 10f;
                var startEasy = this.transform.position + (startPoint - currentPoint);
                var endEasy = this.transform.position;
                if (!HardMode)
                {
                    Tl.RenderLine(startEasy, endEasy);
                }
                else if (HardMode)
                {
                    Tl.RenderLine(startPoint, currentPoint);
                }
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        startPoint = cam.ScreenToWorldPoint(touch.position);
                        startPoint.z = 10f;
                        break;
                    case TouchPhase.Ended:
                        endPoint = cam.ScreenToWorldPoint(touch.position);
                        endPoint.z = 10f;

                        force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));
                        rb.velocity = new Vector2(0, 0); //I added this line
                        rb.AddForce(force * power, ForceMode2D.Impulse);
                        Tl.EndLine();
                        break;
                }
            }
        }

        if (isMouseTesting)
        {
            if (Input.GetMouseButtonDown(0))
            {
                startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                startPoint.z = 10f;
            }
            if (Input.GetMouseButton(0))
            {
                Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                currentPoint.z = 10f;
                var startEasy = this.transform.position + (startPoint - currentPoint);
                var endEasy = this.transform.position;
                if (!HardMode)
                {
                    Tl.RenderLine(startEasy, endEasy);
                }
                else if (HardMode)
                {
                    Tl.RenderLine(startPoint, currentPoint);
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                endPoint.z = 10f;
                force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));
                rb.velocity = new Vector2(0, 0); //I added this line
                rb.AddForce(force * power, ForceMode2D.Impulse);
                Tl.EndLine();
            }
        }
    }

    private void StopBall()
    {
        rb.velocity = new Vector2(0, 0);
    }
}
