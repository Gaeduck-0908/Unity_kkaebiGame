using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sp_atk : MonoBehaviour
{
    IEnumerator Atk()
    {
        yield return new WaitForSeconds(1.5f);
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        switch(col.tag)
        {
            case "Enemy":
                if (this.gameObject.activeInHierarchy)
                {
                    StartCoroutine("Atk");
                }
                Enemy.Instance.hp -= 100;
                break;
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
