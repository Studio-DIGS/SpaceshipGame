using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectOnPath : MonoBehaviour
{
    public bool onPath;
    [HideInInspector] public Vector3 target;
    [HideInInspector] public Vector2 move, targetDir;
}
