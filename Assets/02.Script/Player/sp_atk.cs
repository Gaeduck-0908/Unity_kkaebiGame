using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//스킬 관련 스크립트
public class sp_atk : MonoBehaviour
{
    //코루틴
    IEnumerator Atk()
    {
        yield return new WaitForSeconds(1.5f); //1.5초 대기
    }

    //충돌 관련
    private void OnTriggerExit2D(Collider2D col)
    {
        switch(col.tag)
        {
            // 적군과 닿을시
            case "Enemy":
                if (this.gameObject.activeInHierarchy)
                {
                    StartCoroutine("Atk");
                }
                Enemy.Instance.hp -= 100;
                break;
            // 보스와 닿을시
            case "Boss":
                if (this.gameObject.activeInHierarchy)
                {
                    StartCoroutine("Atk");
                }
                Boss.Instance.hp -= 200;
                break;
        }
    }
}
