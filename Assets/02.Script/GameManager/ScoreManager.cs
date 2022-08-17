using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    //점수 텍스트
    [SerializeField]
    private Text scoreText;
    //이름 텍스트
    [SerializeField]
    private InputField inputName;

    //점수
    private int score;

    //1등 텍스트
    [SerializeField]
    private Text first_t;

    //2등 텍스트
    [SerializeField]
    private Text second_t;

    //3등 텍스트
    [SerializeField]
    private Text third_t;

    private void Start()
    {
        //초기화 코드
        //PlayerPrefs.DeleteAll();
        /*PlayerPrefs.SetInt("Rank1", 300);
        PlayerPrefs.SetInt("Rank2", 200);
        PlayerPrefs.SetInt("Rank3", 100);

        PlayerPrefs.SetString("Rank1_name", "AAA");
        PlayerPrefs.SetString("Rank2_name", "BBB");
        PlayerPrefs.SetString("Rank3_name", "CCC");*/

        //일시정지 해제
        Time.timeScale = 1;

        //저장된 데이터 불러오기(체력)
        score = 500 + PlayerPrefs.GetInt("HP");

        //씬 판별후 코드 적용
        if (SceneManager.GetActiveScene().name == "03.End")
        {
            scoreText.text = "Your Score : " + score;
        }

        if(SceneManager.GetActiveScene().name == "04.Ranking" || SceneManager.GetActiveScene().name == "05.Ranking")
        {
            first_t.text = "Ranking Top 1 : " + PlayerPrefs.GetString("Rank1_name") + ":" + PlayerPrefs.GetInt("Rank1") + ".";
            second_t.text = "Ranking Top 2 : " + PlayerPrefs.GetString("Rank2_name") + ":" + PlayerPrefs.GetInt("Rank2") + ".";
            third_t.text = "Ranking Top 3 : " + PlayerPrefs.GetString("Rank3_name") + ":" + PlayerPrefs.GetInt("Rank3") + ".";
        }
    }

    //적용 버튼
    public void submit()
    {
        string temp = inputName.text;

        if(temp.Length > 3)
        {
            temp = temp.Substring(0, 3);
        }

        insert(temp);
        commit();
    }

    //데이터 삽입
    private void insert(string temp)
    {
        //등수 변경
        if(PlayerPrefs.GetInt("Rank1") <= score)
        {
            PlayerPrefs.SetInt("Rank3", PlayerPrefs.GetInt("Rank2"));
            PlayerPrefs.SetString("Rank3_name", PlayerPrefs.GetString("Rank2_name"));

            PlayerPrefs.SetInt("Rank2", PlayerPrefs.GetInt("Rank1"));
            PlayerPrefs.SetString("Rank2_name", PlayerPrefs.GetString("Rank1_name"));

            PlayerPrefs.SetInt("Rank1", score);
            PlayerPrefs.SetString("Rank1_name", temp);

        }
        else if (PlayerPrefs.GetInt("Rank2") <= score)
        {
            PlayerPrefs.SetInt("Rank3", PlayerPrefs.GetInt("Rank2"));
            PlayerPrefs.SetString("Rank3_name", PlayerPrefs.GetString("Rank2_name"));

            PlayerPrefs.SetInt("Rank2", score);
            PlayerPrefs.SetString("Rank2_name", temp);
        }
        else if (PlayerPrefs.GetInt("Rank3") <= score)
        {
            PlayerPrefs.SetInt("Rank3", score);
            PlayerPrefs.SetString("Rank3_name", temp);
        }
    }

    //커밋
    private void commit()
    {
        first_t.text = "Ranking Top 1 : " + PlayerPrefs.GetString("Rank1_name") + ":" + PlayerPrefs.GetInt("Rank1") + ".";
        second_t.text = "Ranking Top 2 : " + PlayerPrefs.GetString("Rank2_name") + ":" + PlayerPrefs.GetInt("Rank2") + ".";
        third_t.text = "Ranking Top 3 : " + PlayerPrefs.GetString("Rank3_name") + ":" + PlayerPrefs.GetInt("Rank3") + ".";
    }
}
