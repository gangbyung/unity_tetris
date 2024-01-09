using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public void Puse()
    {
        Time.timeScale = 0f;
    }
    public void GameStart()
    {
        Time.timeScale = 1f;
    }
}
