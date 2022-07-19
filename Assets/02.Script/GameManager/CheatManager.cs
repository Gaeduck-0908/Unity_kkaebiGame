using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatManager : MonoBehaviour
{
    private void Update()
    {
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

        else if (Input.GetKeyDown(KeyCode.F2))
        {
            if(PlayerManager.Instance.level <= 4)
            {
                PlayerManager.Instance.level++;
                GameManager.Instance.ItemPickPanel.SetActive(true);
            }
        }

        else if (Input.GetKeyDown(KeyCode.F3))
        {
            for (int i = 0; i <= 10; i++)
            {
                Destroy(GameObject.FindGameObjectWithTag("Enemy"));
                Destroy(GameObject.FindGameObjectWithTag("Boss"));
            }
        }

        else if (Input.GetKeyDown(KeyCode.F4))
        {
            SceneManager.LoadScene("01.Title");
        }

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
