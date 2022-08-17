using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//치트 관련 스크립트
public class CheatManager : MonoBehaviour
{
    //프레임 단위 실행
    private void Update()
    {
        //무적 토글 치트
        if(Input.GetKeyDown(KeyCode.F1))
        {
            if (PlayerManager.Instance.Player_god == true)
            {
                PlayerManager.Instance.Player_god = false;
            }
            else
            {
                PlayerManager.Instance.Player_god = true;
            }
        }

        //레벨업 치트
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            if(PlayerManager.Instance.level <= 4)
            {
                PlayerManager.Instance.level++;
                GameManager.Instance.ItemPickPanel.SetActive(true);
            }
        }

        //적군 제거 치트
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            for (int i = 0; i <= 10; i++)
            {
                Destroy(GameObject.FindGameObjectWithTag("Enemy"));
                Destroy(GameObject.FindGameObjectWithTag("Boss"));
            }
        }

        //타이틀 이동 치트
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            SceneManager.LoadScene("01.Title");
        }

        //일시정지 치트
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            else Time.timeScale = 0;
        }
    }
}
