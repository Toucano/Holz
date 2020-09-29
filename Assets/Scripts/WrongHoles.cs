using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WrongHoles : MonoBehaviour
{
    GameStatus gamestatus;

    private void Awake()
    {
        GameEvents.current.onBallInRightHole += DestroyHole;
    }

    private void Start()
    {
        gamestatus = FindObjectOfType<GameStatus>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gamestatus.ScoreManagement();
        if(collision.tag == "HoleDetector")
        {
            GameEvents.current.BallInWrongHole();
            GameStatus.IsDead = true;
            gamestatus.holesEnteredScore -= 1;
        }
    }


    private void DestroyHole()
    {
        if (this != null)
        {
            Destroy(gameObject);
        }
    }
}
