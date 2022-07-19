using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_Manager : MonoBehaviour
{
    public void goto_1()
    {
        SceneManager.LoadScene("02.Stage1");
    }

    public void goto_Ranking2()
    {
        SceneManager.LoadScene("05.Ranking");
    }

    public void goto_Ranking()
    {
        SceneManager.LoadScene("04.Ranking");
    }

    public void goto_Main()
    {
        SceneManager.LoadScene("01.Title");
    }
}
