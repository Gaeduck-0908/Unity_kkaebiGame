using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//씬 관련 스크립트
public class Title_Manager : MonoBehaviour
{
    //02.Stage1 이동
    public void goto_1()
    {
        SceneManager.LoadScene("02.Stage1");
    }

    //05.Ranking 이동
    public void goto_Ranking2()
    {
        SceneManager.LoadScene("05.Ranking");
    }

    //04.Ranking 이동
    public void goto_Ranking()
    {
        SceneManager.LoadScene("04.Ranking");
    }

    //01.Title 이동
    public void goto_Main()
    {
        SceneManager.LoadScene("01.Title");
    }
}
