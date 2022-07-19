using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using lib;

public class Boss : Singleton<Boss>
{
    [SerializeField]
    private GameObject boss_bullet;

    [SerializeField]
    private GameObject boss_idle;
    [SerializeField]
    private GameObject boss_attack;
    [SerializeField]
    private GameObject boss_death;

    [SerializeField]
    private GameObject boss_shot;

    [SerializeField]
    private GameObject target;

    [SerializeField]
    private Text Boss_hp_t;

    [SerializeField]
    private GameObject clear_panel;

    public int hp = 1000;
    public int dmg = 15;

    private void Start()
    {
        GameManager.Instance.boss_isLiving = true;
        Boss_hp_t.gameObject.SetActive(true);
        GameManager.Instance.gameObject.GetComponent<AudioSource>().clip = GameManager.Instance.boss_bgm;
        GameManager.Instance.gameObject.GetComponent<AudioSource>().Play();
        target = GameObject.FindGameObjectWithTag("Player");
        hp = 1000;
        dmg = 20;
        boss_idle.SetActive(true);
        boss_attack.SetActive(false);
        boss_death.SetActive(false);

        StartCoroutine("bossPattern");
    }


    private void Update()
    {
        Boss_hp_t.text = "HP : " + hp;
        if (target.transform.position.x > this.transform.position.x)
        {
            this.transform.GetComponentInChildren<SpriteRenderer>().flipX = false;
        }
        else
        {
            this.transform.GetComponentInChildren<SpriteRenderer>().flipX = true;
        }

        if (hp <= 0)
        {
            GameManager.Instance.boss_isLiving = false;
            GameManager.Instance.gameObject.GetComponent<AudioSource>().clip = GameManager.Instance.main_bgm;
            GameManager.Instance.gameObject.GetComponent<AudioSource>().Play();
            PlayerManager.Instance.exp += 100;
            PlayerManager.Instance.hp += 300;
            clear_panel.SetActive(true);
            Time.timeScale = 0;
            Destroy(this.gameObject);
        }
    }

    IEnumerator bossPattern()
    {
        while (true)
        {
            int rd = Random.Range(1, 5);
            Debug.Log(rd);

            switch (rd)
            {
                case 1:
                    for (int i = 1; i <= 10; i++)
                    {
                        for (int j = 0; j <= 50; j += 10)
                        {
                            Instantiate(boss_bullet, boss_shot.transform.position, Quaternion.Euler(0, 0, j));
                            boss_idle.SetActive(false);
                            boss_attack.SetActive(true);
                            boss_death.SetActive(false);
                            yield return new WaitForSeconds(0.1f);
                        }
                    }
                    boss_idle.SetActive(true);
                    boss_attack.SetActive(false);
                    boss_death.SetActive(false);
                    break;
                case 2:
                    for (int j = 1; j <= 5; j++)
                    {
                        for (int i = 0; i <= 360; i += 20)
                        {
                            Instantiate(boss_bullet, boss_shot.transform.position, Quaternion.Euler(0, 0, i));
                            boss_idle.SetActive(false);
                            boss_attack.SetActive(true);
                            boss_death.SetActive(false);
                            yield return new WaitForSeconds(0.1f);
                        }
                    }
                    boss_idle.SetActive(true);
                    boss_attack.SetActive(false);
                    boss_death.SetActive(false);
                    break;
                case 3:
                    for (int j = 1; j <= 5; j++)
                    {
                        for (int i = 0; i <= 360; i += 20)
                        {
                            Instantiate(boss_bullet, boss_shot.transform.position, Quaternion.Euler(0, 0, i));
                            boss_idle.SetActive(false);
                            boss_attack.SetActive(true);
                            boss_death.SetActive(false);
                        }
                        yield return new WaitForSeconds(0.5f);
                    }
                    boss_idle.SetActive(true);
                    boss_attack.SetActive(false);
                    boss_death.SetActive(false);
                    break;
                case 4:
                    for (int j = 1; j <= 5; j++)
                    {
                        for (int i = 360; i >= 0; i -= 20)
                        {
                            Instantiate(boss_bullet, new Vector3(boss_shot.transform.position.x - 1, boss_shot.transform.position.y, boss_shot.transform.position.z), Quaternion.Euler(0, 0, i));
                            boss_idle.SetActive(false);
                            boss_attack.SetActive(true);
                            boss_death.SetActive(false);
                        }
                        yield return new WaitForSeconds(0.3f);
                        for (int i = 0; i <= 360; i += 10)
                        {
                            Instantiate(boss_bullet, new Vector3(boss_shot.transform.position.x + 1, boss_shot.transform.position.y, boss_shot.transform.position.z), Quaternion.Euler(0, 0, i));
                            boss_idle.SetActive(false);
                            boss_attack.SetActive(true);
                            boss_death.SetActive(false);
                        }
                        yield return new WaitForSeconds(0.3f);
                    }

                    boss_idle.SetActive(true);
                    boss_attack.SetActive(false);
                    boss_death.SetActive(false);
                    break;
            }

            yield return new WaitForSeconds(8f);
        }
    }
}
