using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointsText : MonoBehaviour
{
    private TMP_Text text;
    private Points points;


    private void Awake() 
    {
        text = gameObject.GetComponent<TMP_Text>();
        points = GameObject.Find("Player").GetComponent<Points>();
    }

    void Update()
    {
        text.SetText("Points: " + points.GetPoints());
    }
}
