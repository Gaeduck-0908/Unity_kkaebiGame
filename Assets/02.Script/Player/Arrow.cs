using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using lib;

public class Arrow : Singleton<Arrow>
{
    private Vector2 movedir;

    private float Speed = 11.0f;

    public int now_dmg = 0;

    private void Start()
    {
        if (PlayerManager.Instance.atk_1.GetComponent<SpriteRenderer>().flipX == true)
        {
            movedir = Vector2.left;
        }
        else
        {
            movedir = Vector2.right;
        }

        switch (PlayerManager.Instance.level)
        {
            case 1:
                now_dmg = PlayerManager.Instance.lv_1_dmg;
                break;
            case 2:
                now_dmg = PlayerManager.Instance.lv_2_dmg;
                break;
            case 3:
                now_dmg = PlayerManager.Instance.lv_3_dmg;
                break;
            case 4:
                now_dmg = PlayerManager.Instance.lv_4_dmg;
                break;
            case 5:
                now_dmg = PlayerManager.Instance.lv_5_dmg;
                break;
        }
    }

    private void Update()
    {
        transform.Translate(movedir * Speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        switch(col.tag)
        {
            case "Enemy":
                Debug.Log(now_dmg);
                Enemy.Instance.hp -= now_dmg;
                if(PlayerManager.Instance.is_spear == false)
                {
                    Destroy(this.gameObject);
                }
                break;

            case "Boss":
                Boss.Instance.hp -= now_dmg;
                if(PlayerManager.Instance.is_spear == false)
                {
                    Destroy(this.gameObject);
                }
                break;
            case "floor":
                Destroy(this.gameObject);
                break;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
