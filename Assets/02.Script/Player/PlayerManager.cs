using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using lib;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField]
    public GameObject idle;
    [SerializeField]
    public GameObject run;
    [SerializeField]
    GameObject jump_up;
    [SerializeField]
    GameObject jump_down;
    [SerializeField]
    GameObject Roll;
    [SerializeField]
    public GameObject atk_1;
    [SerializeField]
    GameObject atk_2;
    [SerializeField]
    GameObject Death;

    [SerializeField]
    private Text cool_text;

    [SerializeField]
    private AudioClip atk_sound;

    [SerializeField]
    private AudioClip jump_sound;

    [SerializeField]
    public AudioClip hit_sound;

    [SerializeField]
    private AudioClip roll_sound;

    Rigidbody2D rig;

    public int level;
    public float exp;

    public int maxhp;
    public int hp;
    public float speed = 3.0f;
    public int atk_dmg;
    public float atk_speed;

    public int lv_1_dmg = 10;
    public int lv_2_dmg = 20;
    public int lv_3_dmg = 30;
    public int lv_4_dmg = 40;
    public int lv_5_dmg = 60;

    [SerializeField]
    private GameObject Player_arrow;

    public bool Player_god;
    private bool is_ground;
    private float jump_speed = 8.5f;

    private bool is_atk;
    private bool is_roll;

    public bool is_spear;

    private bool cooltime = true;
    private int sp_atk_cool = 10;
    private bool cooldown = true;

    private bool is_living;

    [SerializeField]
    private GameObject[] Enemy_list; 
    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        maxhp = 100;
        level = 1;
        exp = 0;
        hp = 100;
        atk_dmg = 10;
        atk_speed = 1.0f;
        Player_god = false;
        is_atk = false;
        is_spear = false;
        cooltime = true;
        is_living = true;
    }

    private void Update()
    {
        Hp_Check();
        if(is_living == true)
        {
            Player_Move();
            Player_Atk();
            Player_Lv_Check();

            switch(level)
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
        if(sp_atk_cool == 10)
        {
            cool_text.text = "¢Ð";
        }
        else
        {
            cool_text.text = sp_atk_cool.ToString();
        }
    }

    private void Player_Move()
    {
        if(is_atk == false)
        {
            StartCoroutine("Player_Roll");
            if (Input.GetKey(KeyCode.D) && is_roll == false)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                if (is_ground == true)
                {
                    idle.SetActive(false);
                    run.SetActive(true);
                    idle.GetComponent<SpriteRenderer>().flipX = false;
                    run.GetComponent<SpriteRenderer>().flipX = false;
                    jump_up.GetComponent<SpriteRenderer>().flipX = false;
                    jump_down.GetComponent<SpriteRenderer>().flipX = false;
                }
            }
            else if (Input.GetKey(KeyCode.A) && is_roll == false)
            {
                transform.Translate(Vector2.left * speed * Time.deltaTime);

                if (is_ground == true)
                {
                    idle.SetActive(false);
                    run.SetActive(true);
                    idle.GetComponent<SpriteRenderer>().flipX = true;
                    run.GetComponent<SpriteRenderer>().flipX = true;
                    jump_up.GetComponent<SpriteRenderer>().flipX = true;
                    jump_down.GetComponent<SpriteRenderer>().flipX = true;
                }
            }
            else if (is_ground == true && is_roll == false)
            {
                idle.SetActive(true);
                run.SetActive(false);
                jump_up.SetActive(false);
                jump_down.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.Space) && is_roll == false)
            {
                if (is_ground == true)
                {
                    idle.SetActive(false);
                    run.SetActive(false);

                    rig.AddForce(Vector2.up * jump_speed, ForceMode2D.Impulse);
                    this.gameObject.GetComponent<AudioSource>().clip = jump_sound;
                    this.gameObject.GetComponent<AudioSource>().Play();

                    jump_up.SetActive(true);

                    is_ground = false;
                }
            }

            if (is_ground == false)
            {
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
    private void Player_Atk()
    {
        StartCoroutine("Player_atk_1");
    }
    
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

    private void Player_Lv_Check()
    {
        if(exp < 0)
        {
            exp = 0;
        }

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

    private void Hp_Check()
    {
        if(hp <= 0)
        {
            StartCoroutine("Player_Death");
        }
    }

    IEnumerator Player_Death()
    {
        is_living = false;
        idle.SetActive(false);
        run.SetActive(false);
        Death.SetActive(true);

        yield return new WaitForSeconds(1.0f);
        Death.SetActive(false);
        hp -= 100;
        PlayerPrefs.SetInt("HP", hp);
        GameManager.Instance.Death_panel.SetActive(true);
        Time.timeScale = 0;
    }

    IEnumerator Player_Roll()
    {
        if(is_ground == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift) && cooltime == true)
            {
                is_roll = true;
                Roll.SetActive(true);
                idle.SetActive(false);
                run.SetActive(false);

                if (idle.GetComponent<SpriteRenderer>().flipX == true || run.GetComponent<SpriteRenderer>().flipX == true)
                {
                    Roll.GetComponent<SpriteRenderer>().flipX = true;
                    rig.AddForce(Vector2.left * 7.5f, ForceMode2D.Impulse);
                }
                else
                {
                    Roll.GetComponent<SpriteRenderer>().flipX = false;
                    rig.AddForce(Vector2.right * 7.5f, ForceMode2D.Impulse);
                }

                this.gameObject.GetComponent<AudioSource>().clip = roll_sound;
                this.gameObject.GetComponent<AudioSource>().Play();

                cooltime = false;
                Player_god = true;
                yield return new WaitForSeconds(0.5f);
                Roll.SetActive(false);
                Player_god = false;
                is_roll = false;
                yield return new WaitForSeconds(1.0f);
                cooltime = true;
            }
        }
    }

    IEnumerator Player_atk_1()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && is_ground == true && cooldown == true)
        {
            yield return new WaitForSeconds(0.1f);
            cooldown = false;
            atk_1.SetActive(true);
            atk_2.SetActive(false);
            idle.SetActive(false);
            run.SetActive(false);

            atk_1.GetComponent<SpriteRenderer>().flipX = false;

            is_atk = true;
            yield return new WaitForSeconds(atk_speed);
            Instantiate(Player_arrow, this.gameObject.transform);
            this.gameObject.GetComponent<AudioSource>().clip = atk_sound;
            this.gameObject.GetComponent<AudioSource>().Play();
            atk_1.SetActive(false);
            is_atk = false;
            cooldown = true;
            yield return new WaitForSeconds(0.1f);
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow) && is_ground == true && cooldown == true)
        {
            yield return new WaitForSeconds(0.1f);
            cooldown = false;
            atk_1.SetActive(true);
            atk_2.SetActive(false);
            idle.SetActive(false);
            run.SetActive(false);

            atk_1.GetComponent<SpriteRenderer>().flipX = true;

            is_atk = true;
            yield return new WaitForSeconds(atk_speed);
            Instantiate(Player_arrow, this.gameObject.transform);
            this.gameObject.GetComponent<AudioSource>().clip = atk_sound;
            this.gameObject.GetComponent<AudioSource>().Play();
            atk_1.SetActive(false);
            is_atk = false;
            cooldown = true;
            yield return new WaitForSeconds(0.1f);
        }

        else if (Input.GetKeyDown(KeyCode.R) && is_ground == true && sp_atk_cool == 10)        
        {
          
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
            atk_2.SetActive(false);
            is_atk = false;
            Player_god = false;
            for(int i = 1; i <= 10; i++)
            {
                sp_atk_cool++;
                yield return new WaitForSeconds(1.0f);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        switch (col.collider.tag)
        {
            case "floor":
                is_ground = true;
                jump_up.SetActive(false);
                jump_down.SetActive(false);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Check_1":
                StartCoroutine("check_1_on");
                break;
            case "Check_2":
                GameManager.Instance.boss.SetActive(true);
                Destroy(col.gameObject);
                break;
            case "i_spear":
                is_spear = true;
                atk_dmg += 10;
                Destroy(col.gameObject);
                break;
            case "i_poision":
                speed += 0.5f;
                Destroy(col.gameObject);
                break;
            case "i_apple":
                maxhp += 10;
                Destroy(col.gameObject);
                break;
            case "i_postion":
                hp += (hp / 5);
                Destroy(col.gameObject);
                break;
            case "i_pie":
                atk_speed -= 0.2f;
                Destroy(col.gameObject);
                break;
        }
    }

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
