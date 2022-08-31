using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHolder : MonoBehaviour
{
    private Points finalScore;

    public void SetScore(Points _points)
    {
        finalScore = _points;
    }
    public int GetScore()
    {
        return finalScore.GetPoints();
    }
}
