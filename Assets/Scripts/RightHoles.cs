using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class RightHoles : MonoBehaviour
{
    GameStatus worldInit;
    public float startTimer;
    float timeToDeath;
 

    private void Start()
    {
        worldInit = FindObjectOfType<GameStatus>();
        timeToDeath = worldInit.healthPoints;
        startTimer = Time.time;
        worldInit.healthBarTimer.SetHealth(worldInit.healthPoints);
    }
    private void Update()
    {
        HealthManagement();
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "HoleDetector")
        {
            GameEvents.current.BallInRightHole();
            StartCoroutine(SpawnAfterTransition());
            worldInit.holesEnteredScore += 1;
        }
    }
    
    private void HealthManagement()
    {
        if (!PauseMenu.GameIsPaused && !GameStatus.InTransition)
        {
            float currentHealth = (startTimer - Time.time) + timeToDeath;
            worldInit.healthBarTimer.SetHealth(currentHealth);
            if (currentHealth <= 0)
            {
                worldInit.holesEnteredScore -= 1;
                worldInit.ScoreManagement();
                GameEvents.current.TimeEnded();
            }
        }
        
    }

    IEnumerator SpawnAfterTransition()
    {
        while (true)
        {
            if (GameStatus.InTransition == false)
            {
                worldInit.SpawnOnProgression();
                Destroy(gameObject);
            }
            yield return null;
        }
        //Debug.Log(WorldInit.InTransition);
        //Debug.Log(Camera.main.orthographicSize);
    }
}
