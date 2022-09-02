using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    //Line Renderers will have2 indexes: Starting Position (i=0) & End Position (i=1)
    [SerializeField] LineRenderer warningLine;
    [SerializeField] LineRenderer laserLine;

    private readonly int startingIndex = 0;
    private readonly int endingIndex = 1;

    public Light light;

    private void Awake()
    {
        warningLine.enabled = false;
        laserLine.enabled = false;
        light.enabled = false;
    }

    public void DisplayWarningLine(Vector3 _startingPosition, Vector3 _finalPosition)
    {
        warningLine.SetPosition(startingIndex, _startingPosition);
        warningLine.SetPosition(endingIndex, _finalPosition);
    }

    public void ShootLaser(Vector3 _startingPosition, Vector3 _finalPosition)
    {
        light.enabled = !light.enabled;
        laserLine.SetPosition(startingIndex, _startingPosition);
        laserLine.SetPosition(endingIndex, _finalPosition);
    }

    public void RemoveLasers()
    {
        warningLine.enabled = false;
        laserLine.enabled = false;
    }

    public void EnableWarningLine()
    {
        warningLine.enabled = true;
        laserLine.enabled = false;
    }

    public void EnableLaserLine()
    {
        warningLine.enabled = false;
        laserLine.enabled = true;
    }
}
