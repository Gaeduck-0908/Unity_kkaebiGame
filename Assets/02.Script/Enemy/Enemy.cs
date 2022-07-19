using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using lib;

public class Enemy : Singleton<Enemy>
{
    [SerializeField]
    private GameObject target;

    [SerializeField]
    private GameObject muzzle;

    [SerializeField]
    private GameObject death;

    public int hp = 30;

    [SerializeField]
    private GameObject Enemy_bullet;

    private void Start()
    {
        StartCoroutine("spawn_bullet");
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (hp <= 0)
        {
            StartCoroutine("Death");
        }

        if(target.transform.position.x > this.transform.position.x)
        {
            this.transform.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            this.transform.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    IEnumerator spawn_bullet()
    {
        while (hp > 0)
        {
            for(int i = 1; i <= 3; i++)
            {
                Instantiate(Enemy_bullet, this.transform);
                this.gameObject.GetComponent<AudioSource>().Play();
                muzzle.SetActive(true);
                yield return new WaitForSeconds(0.3f);
                muzzle.SetActive(false);
            }
            yield return new WaitForSeconds(2.0f);
        }
    }

    IEnumerator Death()
    {
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        death.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        int temp = Random.Range(0, 5);

        int exp = Random.Range(40, 60);

        PlayerManager.Instance.exp += exp;

        Debug.Log(temp);
        switch (temp)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
                Instantiate(GameManager.Instance.ItemList[temp], this.transform.position, Quaternion.Euler(0,0,0));
                break;

            default:
                break;
        }

        Destroy(this.gameObject);
    }
}
