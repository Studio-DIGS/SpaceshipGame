using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    private int points = 0;

    public void AddPoints(int _points)
    {
        points += _points;
    }

    public void SubtractPoints(int _points)
    {
        points -= _points;
    }

    public int GetPoints() 
    {
        return points;
    }
    public void SetPoints(int _points)
    {
        points = _points;
    }
}
