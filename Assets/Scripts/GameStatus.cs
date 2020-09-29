using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GameStatus : MonoBehaviour
{

    public int holesEnteredScore = 0;

    //[SerializeField] float qualityCursor = 0.8f;

    [Range(0.5f, 5f)] public float nonSpawnRadiusRightHole = 1f;
    [Range(0.5f, 5f)] public float nonSpawnRadiusWrongHole = 1f;

    [SerializeField] public int healthPoints = 5;

    GameObject topWall;
    GameObject bottomWall;
    GameObject RightWall;
    GameObject LeftWall;


    [SerializeField] GameObject borders;
    public GameObject wrongHoles;
    public GameObject rightHole;
    [SerializeField] LayerMask nonSpawnableForHoles;
    [SerializeField] LayerMask whatAreHoles;
    [SerializeField] TextMeshProUGUI holesEnteredText;
    public GameObject DeathMenuUI;
    public HealthBarTimer healthBarTimer;
    [SerializeField] CameraAnimations camAnim;
    [SerializeField] RightHoles rightMecanics;

    public static bool InTransition = false;
    public static bool IsDead = false;

    [SerializeField] Transform ball;

    Transform rightHolePos;

    //[SerializeField] GameObject PostProcess;
    //public bool DoZoomAnim = false;

    //[SerializeField] Camera cam1;
    //[SerializeField] Camera cam2;




    private void Awake()
    {
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        //if (Settings.Quality == "fast")
        //{
        //    //PostProcess.SetActive(false);
        //    Camera.main.GetComponent<PostProcessLayer>().enabled = false;
        //}
        //else if (Settings.Quality == "high")
        //{
        //    //PostProcess.SetActive(true);
        //    Camera.main.GetComponent<PostProcessLayer>().enabled = true;

        //}
        GameEvents.current.onBallInWrongHole += ActivateDeathMenu;
        GameEvents.current.onTimeEnded += ActivateDeathMenu;
        GameEvents.current.onBallInRightHole += StartZoomCoroutine;

        holesEnteredScore = PlayerPrefs.GetInt("Highscore", 0);

        Time.timeScale = 1;
        MakeWallsBorder();
        SpawnOnProgression();
    }

    

    void Start()
    {
        IsDead = false;
        healthBarTimer = FindObjectOfType<HealthBarTimer>();
        healthBarTimer.SetMaxHealth(healthPoints);
    }


    public void RandomSpawning(GameObject hole, float nonSpawnRadius)
    {
        float worldHeight = Camera.main.orthographicSize;
        float worldWidth = worldHeight * Camera.main.aspect;
        int NoCrash = 0;
        float objectRadius = (hole.GetComponent<SpriteRenderer>().bounds.size.x);

        while (true)
        {
            var randomPos = new Vector2(Random.Range(-worldWidth+objectRadius, worldWidth-objectRadius),
                Random.Range(-worldHeight+objectRadius, worldHeight-objectRadius));
            NoCrash++;
            if (Physics2D.OverlapCircle(randomPos, nonSpawnRadius, nonSpawnableForHoles) == null)
            {
                if (Physics2D.OverlapCircle(randomPos, objectRadius, whatAreHoles) == null)
                {
                    var temphole = Instantiate(hole, randomPos, Quaternion.identity);
                    break;
                }
            }
            else
            {
                randomPos = new Vector2(Random.Range(-worldWidth, worldWidth),
                    Random.Range(-worldHeight, worldHeight));
            }
            

            if (NoCrash > 10000)
            {
                Instantiate(hole, randomPos, Quaternion.identity);
                break;
            }
        }
    }
    private void MakeWallsBorder()
        {
            float worldHeight = Camera.main.orthographicSize;
            float worldWidth = worldHeight * Camera.main.aspect;

            topWall = Instantiate(borders);
            Vector3 borderLenght = topWall.GetComponent<Collider2D>().bounds.size;
            topWall.transform.position = new Vector3(0, worldHeight + borderLenght.y / 2, 0);
            bottomWall = Instantiate(borders);
            bottomWall.transform.position = new Vector3(0, -(worldHeight + borderLenght.y / 2), 0);
            RightWall = Instantiate(borders);

            RightWall.transform.position = new Vector3(worldWidth + borderLenght.x / 2, 0, 0);
            LeftWall = Instantiate(borders);
            LeftWall.transform.position = new Vector3(-(worldWidth + borderLenght.x / 2), 0, 0);
        }
    public void ActivateDeathMenu()
    {
        Time.timeScale = 0;
        DeathMenuUI.SetActive(true);    
    }

    public IEnumerator ZoomInAndOut()
    {
        rightHolePos = GameObject.FindGameObjectWithTag("righthole").transform;
        while (!CameraAnimations.IsZoomedOut)
        {
            ball.transform.position = new Vector3(Mathf.Lerp(ball.transform.position.x, rightHolePos.transform.position.x, Time.deltaTime * 4),
                Mathf.Lerp(ball.transform.position.y, rightHolePos.transform.position.y, Time.deltaTime * 4),
                ball.transform.position.z);
            camAnim.ZoomInCamera();
            yield return CameraAnimations.IsZoomedIn == true;
            camAnim.ZoomOutCamera();
            yield return CameraAnimations.IsZoomedIn == false;
        }

    }

    private void StartZoomCoroutine()
    {
        InTransition = true;
        CameraAnimations.IsZoomedOut = false;
        StartCoroutine(ZoomInAndOut());
    }

    public void SpawnOnProgression()
    {
        ScoreManagement();
        RandomSpawning(rightHole, nonSpawnRadiusRightHole);
        for (int i = 0; i < Mathf.RoundToInt((-500 / ((holesEnteredScore) + 100)) + 5); i++)
        {
            RandomSpawning(wrongHoles, nonSpawnRadiusWrongHole);
        }
    }

    public void ScoreManagement()
    {
        holesEnteredText.text = holesEnteredScore.ToString();
        if (holesEnteredScore != PlayerPrefs.GetInt("Highscore", 0))
        {
            PlayerPrefs.SetInt("Highscore", holesEnteredScore);
        }
    }
}
