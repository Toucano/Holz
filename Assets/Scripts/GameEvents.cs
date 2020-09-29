using System;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;
    private void Awake()
    {
        current = this;
    }

    public event Action onBallInRightHole;
    public void BallInRightHole()
    {
        if (onBallInRightHole != null)
        {
            onBallInRightHole();
        }
    }
    public event Action onBallInWrongHole;
    public void BallInWrongHole()
    {
        if (onBallInWrongHole != null)
        {
            onBallInWrongHole();
        }
    }
    public event Action onTimeEnded;
    public void TimeEnded()
    {
        if (onTimeEnded != null)
        {
            onTimeEnded();
        }
    }
}
