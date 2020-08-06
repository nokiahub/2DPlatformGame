using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreManager : MonoBehaviour
{

    static int score = 0;

    public static void setScore(int value)
    {
        score += value;
    }

    public static int getScore()
    {
        return score;
    }

    void OnGUI()
    {
        GUILayout.Label("Score : ", score.ToString());
    }
}
