using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sp_atk : MonoBehaviour
{
    IEnumerator atk()
    {
        yield return new WaitForSeconds(1.5f);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        switch(col.tag)
        {
            case "Enemy":
                StartCoroutine("atk");
                Enemy.Instance.hp -= 100;
                break;
            case "Boss":
                StartCoroutine("atk");
                Boss.Instance.hp -= 200;
                break;
        }
    }
}
