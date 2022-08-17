using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using lib;

// 플레이어 관련 스크립트
public class PlayerManager : Singleton<PlayerManager>
{
    // SerializeField 변수를 인스펙터에서 접근가능하게 해주는 기능
    // public 싱글톤 공유 변수,함수
    // private 내부 변수 함수

    [SerializeField]
    public GameObject idle; //서있는 상태
    [SerializeField]
    public GameObject run; //뛰는 상태
    [SerializeField]
    GameObject jump_up; //점프 시작
    [SerializeField]
    GameObject jump_down; //점프 끝
    [SerializeField]
    GameObject Roll; //구르기
    [SerializeField]
    public GameObject atk_1; //공격 1
    [SerializeField]
    GameObject atk_2; //공격 2(스킬)
    [SerializeField]
    GameObject Death; //죽음

    [SerializeField]
    private Text cool_text; //스킬 쿨타임 텍스트

    [SerializeField]
    private AudioClip atk_sound; //공격 소리

    [SerializeField]
    private AudioClip jump_sound; //점프 소리

    [SerializeField]
    public AudioClip hit_sound; //맞는 소리

    [SerializeField]
    private AudioClip roll_sound; //구르기 소리

    Rigidbody2D rig; //Rigidbody2D 컴포넌트

    public int level; //플레이어 레벨
    public float exp; //플레이어 경험치

    public int maxhp; //플레이어 최대 체력
    public int hp; //플레이어 현재 체력
    public float speed = 3.0f; //플레이어 현재 속도
    public int atk_dmg; //플레이어 공격 데미지
    public float atk_speed; //플레이어 공격 속도

    public int lv_1_dmg = 10; //lv1 데미지
    public int lv_2_dmg = 20; //lv2 데미지
    public int lv_3_dmg = 30; //lv3 데미지
    public int lv_4_dmg = 40; //lv4 데미지
    public int lv_5_dmg = 60; //lv5 데미지

    [SerializeField]
    private GameObject Player_arrow; //플레이어 화살

    public bool Player_god; //플레이어 무적 여부
    private bool is_ground; //플레이어 땅 닿아있는지 여부
    private float jump_speed = 8.5f; //플레이어 점프 속도

    private bool is_atk; //플레이어 공격 여부
    private bool is_roll; //플레이어 구르기 여부

    public bool is_spear; //플레이어 관통샷 여부

    private bool cooltime = true; //플레이어 스킬 쿨타임 여부
    private int sp_atk_cool = 10; //플레이어 스킬 쿨타임
    private bool cooldown = true; //플레이어 공격 쿨타임 여부

    private bool is_living; //플레이어 생존 여부

    [SerializeField]
    private GameObject[] Enemy_list;  //적군 리스트
    private void Start() //초기설정
    {
        rig = GetComponent<Rigidbody2D>(); //컴포넌트 찾기 (Rigidzbody2D)
        maxhp = 100; //최대체력 설정
        level = 1; //시작레벨 설정
        exp = 0; //경험치 설정
        hp = 100; //현재 체력 설정
        atk_dmg = 10; //공격력 설정
        atk_speed = 1.0f; //공격 속도 설정
        Player_god = false; //무적여부 설정
        is_atk = false; //공격여부 설정
        is_spear = false; //관통샷 여부 설정
        cooltime = true; //스킬 쿨타임 설정
        is_living = true; //플레이어 생존 여부 설정
    }

    private void Update() //프레임 단위 실행
    {
        Hp_Check(); //체력 체크 함수
        if(is_living == true) //플레이어 생존 여부 확인 (살아있다면 참)
        {
            Player_Move(); //플레이어 움직임 함수
            Player_Atk(); //플레이어 공격 함수
            Player_Lv_Check(); //플레이어 레벨 체크 함수

            switch(level) //레벨에 따른 데미지 설정
            {
                case 1:
                    atk_dmg = lv_1_dmg;
                    break;
                case 2:
                    atk_dmg = lv_2_dmg;
                    break;
                case 3:
                    atk_dmg = lv_3_dmg;
                    break;
                case 4:
                    atk_dmg = lv_4_dmg;
                    break;
                case 5:
                    atk_dmg = lv_5_dmg;
                    break;
            }
        }
        if(sp_atk_cool == 10) //스킬 쿨타임이 되었다면
        {
            cool_text.text = "☜"; //텍스트 변경
        }
        else
        {
            cool_text.text = sp_atk_cool.ToString(); //아니라면 스킬쿨타임 표시
        }
    }

    private void Player_Move() //플레이어 움직임 함수
    {
        if(is_atk == false) //공격중이 아닐떄
        {
            StartCoroutine("Player_Roll"); //구르기 코루틴 시작
            if (Input.GetKey(KeyCode.D) && is_roll == false) //D 오른쪽으로 움직이면서 구르지 않을시
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime); // right 방향으로 speed * 프레임 곱해줌
                if (is_ground == true) //땅에 닿아있다면
                {
                    //플레이어 애니메이션 설정
                    //flip.x or flip.y = true 일시 왼쪽 false 일시 오른쪽
                    idle.SetActive(false);
                    run.SetActive(true);
                    idle.GetComponent<SpriteRenderer>().flipX = false;
                    run.GetComponent<SpriteRenderer>().flipX = false;
                    jump_up.GetComponent<SpriteRenderer>().flipX = false;
                    jump_down.GetComponent<SpriteRenderer>().flipX = false;
                }
            }
            // A 왼쪽으로 움직이면서 구르지 않을시
            else if (Input.GetKey(KeyCode.A) && is_roll == false)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime); // left 방향으로 speed * 프레임 곱해줌

                if (is_ground == true) //땅에 닿아있을시
                {
                    //플레이어 애니메이션 설정
                    //flip.x or flip.y = true 일시 왼쪽 false 일시 오른쪽
                    idle.SetActive(false);
                    run.SetActive(true);
                    idle.GetComponent<SpriteRenderer>().flipX = true;
                    run.GetComponent<SpriteRenderer>().flipX = true;
                    jump_up.GetComponent<SpriteRenderer>().flipX = true;
                    jump_down.GetComponent<SpriteRenderer>().flipX = true;
                }
            }
            // 아무 동작 안하고 서있을시
            else if (is_ground == true && is_roll == false)
            {
                //애니메이션 설정
                idle.SetActive(true);
                run.SetActive(false);
                jump_up.SetActive(false);
                jump_down.SetActive(false);
            }

            // 점프키를 누를시
            if (Input.GetKeyDown(KeyCode.Space) && is_roll == false)
            {
                // 땅에 닿아있는지 체크
                if (is_ground == true)
                {
                    idle.SetActive(false);
                    run.SetActive(false);

                    // up 방향으로 jump_speed 만큼 빠르게 힘을 가해줌
                    rig.AddForce(Vector2.up * jump_speed, ForceMode2D.Impulse);
                    // 사운드 교체,재생
                    this.gameObject.GetComponent<AudioSource>().clip = jump_sound;
                    this.gameObject.GetComponent<AudioSource>().Play();

                    jump_up.SetActive(true);

                    is_ground = false;
                }
            }

            //땅에 닿아있지 않을시
            if (is_ground == false)
            {
                // 애니메이션 관련
                if (Input.GetKeyDown(KeyCode.A))
                {
                    jump_up.GetComponent<SpriteRenderer>().flipX = true;
                    jump_down.GetComponent<SpriteRenderer>().flipX = true;
                }
                else if (Input.GetKeyDown(KeyCode.D))
                {
                    jump_up.GetComponent<SpriteRenderer>().flipX = false;
                    jump_down.GetComponent<SpriteRenderer>().flipX = false;
                }
            }
        }
    }
    // 플레이어 공격 함수
    private void Player_Atk()
    {
        // 코루틴 시작
        StartCoroutine("Player_atk_1");
    }
    
    //소수 판별 함수
    private int isPrime(int temp)
    {
        int cnt = 0;

        for (int i = 2; i < temp; i++)
        {
            if(temp % i == 0)
            {
                continue;
            }
            else
            {
                cnt++;
            }
        }

        return cnt;
    }

    //플레이어 레벨 체크 함수
    private void Player_Lv_Check()
    {
        // 언더플로 방지
        if(exp < 0)
        {
            exp = 0;
        }

        // 경험치 관련 로직
        if(exp >= isPrime(100) && level == 1)
        {
            level++;
            exp -= 100;
            GameManager.Instance.ItemPickPanel.SetActive(true);
        }
        else if (exp >= isPrime(1000) && level == 2)
        {
            level++;
            exp -= 1000;
            GameManager.Instance.ItemPickPanel.SetActive(true);
        }
        else if (exp >= isPrime(10000) && level == 3)
        {
            level++;
            exp -= 10000;
            GameManager.Instance.ItemPickPanel.SetActive(true);
        }
        else if (exp >= isPrime(100000) && level == 4)
        {
            level++;
            exp -= 100000;
            GameManager.Instance.ItemPickPanel.SetActive(true);
        }

    }

    //체력 체크 함수
    private void Hp_Check()
    {
        //체력이 0보다 작을시
        if(hp <= 0)
        {
            //코루틴 시작
            StartCoroutine("Player_Death");
        }
    }

    //플레이어 죽음 코루틴
    IEnumerator Player_Death()
    {
        //애니메이션 여부
        is_living = false;
        idle.SetActive(false);
        run.SetActive(false);
        Death.SetActive(true);

        yield return new WaitForSeconds(1.0f); //1초 대기
        Death.SetActive(false);
        hp -= 100;
        PlayerPrefs.SetInt("HP", hp); // hp 변수 HP라는 명에 저장
        GameManager.Instance.Death_panel.SetActive(true);
        Time.timeScale = 0; //일시정지
    }

    //플레이어 구르기 코루틴
    IEnumerator Player_Roll()
    {
        //땅에 닿아있을시
        if(is_ground == true)
        {
            // 구르기 키 를 누를시
            if (Input.GetKeyDown(KeyCode.LeftShift) && cooltime == true)
            {
                //애니메이션 관련
                is_roll = true;
                Roll.SetActive(true);
                idle.SetActive(false);
                run.SetActive(false);

                if (idle.GetComponent<SpriteRenderer>().flipX == true || run.GetComponent<SpriteRenderer>().flipX == true)
                {
                    Roll.GetComponent<SpriteRenderer>().flipX = true;
                    rig.AddForce(Vector2.left * 7.5f, ForceMode2D.Impulse); //왼쪽 방향으로 7.5 만큼 힘을줌
                }
                else
                {
                    Roll.GetComponent<SpriteRenderer>().flipX = false;
                    rig.AddForce(Vector2.right * 7.5f, ForceMode2D.Impulse); //오른쪽 방향으로 7.5 만큼 힘을줌
                }

                //사운드 교체,재생
                this.gameObject.GetComponent<AudioSource>().clip = roll_sound; 
                this.gameObject.GetComponent<AudioSource>().Play();

                //구르기 로직
                cooltime = false;
                Player_god = true;
                yield return new WaitForSeconds(0.5f); //0.5초 대기
                Roll.SetActive(false);
                Player_god = false;
                is_roll = false;
                yield return new WaitForSeconds(1.0f);
                cooltime = true;
            }
        }
    }

    //플레이어 공격 함수
    IEnumerator Player_atk_1()
    {
        //오른쪽 방향키 누를시
        if (Input.GetKeyDown(KeyCode.RightArrow) && is_ground == true && cooldown == true)
        {
            //애니메이션 관련
            yield return new WaitForSeconds(0.1f); //애니메이션 때문에 0.1초 대기
            cooldown = false;
            atk_1.SetActive(true);
            atk_2.SetActive(false);
            idle.SetActive(false);
            run.SetActive(false);

            atk_1.GetComponent<SpriteRenderer>().flipX = false;

            is_atk = true;
            yield return new WaitForSeconds(atk_speed);
            Instantiate(Player_arrow, this.gameObject.transform); //화살 생성
            //사운드 교체,재생
            this.gameObject.GetComponent<AudioSource>().clip = atk_sound;
            this.gameObject.GetComponent<AudioSource>().Play();
            atk_1.SetActive(false);
            is_atk = false;
            cooldown = true;
            yield return new WaitForSeconds(0.1f);
        }

        //왼쪽 방향키 누를시
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && is_ground == true && cooldown == true)
        {
            yield return new WaitForSeconds(0.1f); //애니메이션 때문에 0.1초 대기
            cooldown = false;
            atk_1.SetActive(true);
            atk_2.SetActive(false);
            idle.SetActive(false);
            run.SetActive(false);

            atk_1.GetComponent<SpriteRenderer>().flipX = true;

            is_atk = true;
            yield return new WaitForSeconds(atk_speed);
            Instantiate(Player_arrow, this.gameObject.transform); //화살 생성
            this.gameObject.GetComponent<AudioSource>().clip = atk_sound;
            this.gameObject.GetComponent<AudioSource>().Play();
            atk_1.SetActive(false);
            is_atk = false;
            cooldown = true;
            yield return new WaitForSeconds(0.1f);
        }

        //스킬 사용시
        else if (Input.GetKeyDown(KeyCode.R) && is_ground == true && sp_atk_cool == 10)        
        {
            //애니메이션 관련
            atk_2.SetActive(true);
            atk_1.SetActive(false);
            idle.SetActive(false);
            run.SetActive(false);
            sp_atk_cool = 0;

            if (idle.GetComponent<SpriteRenderer>().flipX == true || run.GetComponent<SpriteRenderer>().flipX == true)
            {
                atk_2.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                atk_2.GetComponent<SpriteRenderer>().flipX = false;
            }

            is_atk = true;
            Player_god = true;
            yield return new WaitForSeconds(1.5f);
            //사운드 교체,재생
            this.gameObject.GetComponent<AudioSource>().clip = atk_sound;
            this.gameObject.GetComponent<AudioSource>().Play();
            atk_2.SetActive(false);
            is_atk = false;
            Player_god = false;
            //스킬 쿨타임
            for(int i = 1; i <= 10; i++)
            {
                sp_atk_cool++;
                yield return new WaitForSeconds(1.0f);
            }
        }
    }

    //플레이어 충돌 관련
    private void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.collider.tag)
        {
            //바닥에 닿아있을시
            case "floor":
                is_ground = true;
                jump_up.SetActive(false);
                jump_down.SetActive(false);
                break;
        }
    }

    //플에이어 충돌 관련
    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            //적군 생성 체크 오브젝트
            case "Check_1":
                //코루틴 시작
                StartCoroutine("check_1_on");
                break;
            //보스 생성 체크 오브젝트
            case "Check_2":
                GameManager.Instance.boss.SetActive(true);
                Destroy(col.gameObject);
                break;
            //관통 아이템 
            case "i_spear":
                is_spear = true;
                atk_dmg += 10;
                Destroy(col.gameObject);
                break;
            //독 아이템(이속빨라짐)
            case "i_poision":
                speed += 0.5f;
                Destroy(col.gameObject);
                break;
            //사과 아이템(최대체력증가)
            case "i_apple":
                maxhp += 10;
                Destroy(col.gameObject);
                break;
            //포션 아이템(체력회복)
            case "i_postion":
                hp += (hp / 5);
                Destroy(col.gameObject);
                break;
            //파이 아이템(공속증가)
            case "i_pie":
                atk_speed -= 0.2f;
                Destroy(col.gameObject);
                break;
        }
    }

    //check1 오브젝트 함수
    IEnumerator check_1_on()
    {
        GameManager.Instance.Check_1.SetActive(true);
        yield return new WaitForSeconds(2f);
        GameManager.Instance.Check_1.SetActive(false);

        for(int i = 0; i < Enemy_list.Length; i++)
        {
            Enemy_list[i].SetActive(true);
        }
    }
}
