using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using lib;

//화살 관련 스크립트
public class Arrow : Singleton<Arrow>
{
    //화살이 날아갈 방향
    private Vector2 movedir;

    //화살의 속도
    private float Speed = 11.0f;

    //화살의 데미지
    public int now_dmg = 0;

    //초기값 설정
    private void Start()
    {
        // 플레이어가 마지막에 보고있던 방향에 따라 화살의 방향설정
        if (PlayerManager.Instance.atk_1.GetComponent<SpriteRenderer>().flipX == true)
        {
            //left
            movedir = Vector2.left;
        }
        else
        {
            //right
            movedir = Vector2.right;
        }

        //데미지 설정
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

    //프레임단위 실행
    private void Update()
    {
        //진행방향으로 Speed * 프레임 만큼 이동
        transform.Translate(movedir * Speed * Time.deltaTime);
    }

    //충돌 관련
    private void OnTriggerEnter2D(Collider2D col)
    {
        switch(col.tag)
        {
            //적과 충돌시
            case "Enemy":
                Debug.Log(now_dmg);
                Enemy.Instance.hp -= now_dmg; //적군 체력감소
                if(PlayerManager.Instance.is_spear == false) //관통샷 false
                {
                    Destroy(this.gameObject); //오브젝트 삭제
                }
                break;

            //보스와 충돌시
            case "Boss":
                Boss.Instance.hp -= now_dmg;
                if(PlayerManager.Instance.is_spear == false)
                {
                    Destroy(this.gameObject);
                }
                break;
            //바닥,벽 충돌시
            case "floor":
                Destroy(this.gameObject);
                break;
        }
    }

    //카메라에서 사라질시
    private void OnBecameInvisible()
    {
        Destroy(this.gameObject); //오브젝트 삭제
    }
}
