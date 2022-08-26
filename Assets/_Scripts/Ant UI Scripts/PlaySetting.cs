using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlaySetting: MonoBehaviour
{
    public void playSetting ()
    {
        SceneManager.LoadScene(3);
    }
}
