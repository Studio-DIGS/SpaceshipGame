using UnityEngine;
using TMPro;

public class FinalScoreScript : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    private int score;
    private GameObject scoreHolder;

    private void Awake()
    {
        scoreHolder = GameObject.FindWithTag("ScoreHolder");
    }

    private void Start()
    {
        score = scoreHolder.GetComponent<ScoreHolder>().GetScore();
        Destroy(scoreHolder);
        text.SetText("Final Score: " + score);
    }
}
