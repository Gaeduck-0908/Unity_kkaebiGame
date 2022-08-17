using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using lib;

//게임 매니저
public class GameManager : Singleton<GameManager>
{
    // 메인 음악 클립
    [SerializeField]
    public AudioClip main_bgm;

    [SerializeField]
    public GameObject boss; //보스 오브젝트
    public AudioClip boss_bgm; //보스 음악
    public bool boss_isLiving; //보스 생존여부

    [SerializeField]
    private AudioClip death_bgm; //죽었을대 음악

    [SerializeField]
    private Text level; //레벨 텍스트
    [SerializeField]
    private Text exp; //경험치 텍스트
    [SerializeField]
    private Text hp; //체력 텍스트
    [SerializeField]
    private Text dmg; //데미지 텍스트
    [SerializeField]
    private Text atks; //공격속도 텍스트
    [SerializeField]
    private Text speed; //이동속도 테스트
    [SerializeField]
    private Text now_time; //플레이시간 텍스트

    public GameObject Check_1;
    //public GameObject Check_2;

    public GameObject Death_panel; //죽음 패널

    [SerializeField]
    public GameObject ItemPickPanel;
    // 최대체력증가,공속증가,관통공격,독공격,HP20%회복
    public GameObject [] ItemList;

    private float dTime = 0; //임시 변수

    private string temp; // 임시 변수

    //시작 설정
    private void Start()
    {
        boss_isLiving = false;
        //메인 배경음악 교체,재생
        this.gameObject.GetComponent<AudioSource>().clip = main_bgm;
        this.gameObject.GetComponent<AudioSource>().Play();
    }

    //프레임 단위 실행
    private void Update()
    {
        SetText();
        if (Death_panel.activeSelf == true)
        {
            //죽음 음악 교체,재생
            this.gameObject.GetComponent<AudioSource>().clip = death_bgm;
            this.gameObject.GetComponent<AudioSource>().Play();
        }
    }

    //텍스트 적용
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

        //플레이 타임 구하는 알고리즘
        dTime += Time.deltaTime;
        if(dTime >= 180f)
        {
            Death_panel.SetActive(true);
        }

        temp = ((int)dTime).ToString();

        now_time.text = temp +" sec";
    }

    //1번 누를시
    public void choice_1()
    {
        PlayerManager.Instance.hp += 20;
        if (PlayerManager.Instance.hp > PlayerManager.Instance.maxhp)
        {
            PlayerManager.Instance.hp = 100;
        }
        ItemPickPanel.SetActive(false);
    }
    //2번 누를시
    public void choice_2()
    {
        PlayerManager.Instance.maxhp += 30;
        ItemPickPanel.SetActive(false);
    }
    //3번 누를시
    public void choice_3()
    {
        PlayerManager.Instance.atk_speed -= 0.2f;
        ItemPickPanel.SetActive(false);
    }
    //다음 버튼 누를시
    public void nextBtn()
    {
        PlayerPrefs.SetInt("HP", PlayerManager.Instance.hp);
        SceneManager.LoadScene("03.End");
    }
}
