using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using lib;

public class GameManager : Singleton<GameManager>
{

    [SerializeField]
    public AudioClip main_bgm;

    [SerializeField]
    public GameObject boss;
    public AudioClip boss_bgm;
    public bool boss_isLiving;

    [SerializeField]
    private AudioClip death_bgm;


    [SerializeField]
    private Text level;
    [SerializeField]
    private Text exp;
    [SerializeField]
    private Text hp;
    [SerializeField]
    private Text dmg;
    [SerializeField]
    private Text atks;
    [SerializeField]
    private Text speed;
    [SerializeField]
    private Text now_time;

    public GameObject Check_1;
    //public GameObject Check_2;

    public GameObject Death_panel;

    [SerializeField]
    public GameObject ItemPickPanel;
    // 최대체력증가,공속증가,관통공격,독공격,HP20%회복
    public GameObject [] ItemList;

    private float dTime = 0;

    private string temp;


    private void Start()
    {
        boss_isLiving = false;
        this.gameObject.GetComponent<AudioSource>().clip = main_bgm;
        this.gameObject.GetComponent<AudioSource>().Play();
    }

    private void Update()
    {
        SetText();
        if (Death_panel.activeSelf == true)
        {
            this.gameObject.GetComponent<AudioSource>().clip = death_bgm;
            this.gameObject.GetComponent<AudioSource>().Play();
        }
    }

    private void SetText()
    {
        level.text = "Level : " + PlayerManager.Instance.level.ToString();
        if(PlayerManager.Instance.exp < 0)
        {
            PlayerManager.Instance.exp = 0;
        }
        exp.text = "EXP : " + PlayerManager.Instance.exp.ToString() + "%";
        hp.text = "HP : " + PlayerManager.Instance.hp.ToString() + "%";
        dmg.text = "DMG : " + PlayerManager.Instance.atk_dmg.ToString();
        atks.text = "ATKS : " + PlayerManager.Instance.atk_speed.ToString();
        speed.text = "SPEED : " + PlayerManager.Instance.speed.ToString();

        dTime += Time.deltaTime;
        if(dTime >= 180f)
        {
            Death_panel.SetActive(true);
        }

        temp = ((int)dTime).ToString();

        now_time.text = temp +" sec";
    }

    public void choice_1()
    {
        PlayerManager.Instance.hp += 20;
        if (PlayerManager.Instance.hp > PlayerManager.Instance.maxhp)
        {
            PlayerManager.Instance.hp = 100;
        }
        ItemPickPanel.SetActive(false);
    }
    public void choice_2()
    {
        PlayerManager.Instance.maxhp += 30;
        ItemPickPanel.SetActive(false);
    }
    public void choice_3()
    {
        PlayerManager.Instance.atk_speed -= 0.2f;
        ItemPickPanel.SetActive(false);
    }

    public void nextBtn()
    {
        PlayerPrefs.SetInt("HP", PlayerManager.Instance.hp);
        SceneManager.LoadScene("03.End");
    }
}
