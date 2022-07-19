using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bullet : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    private Vector2 movedir;

    private int dmg;

    private float speed = 7.0f;

    private void Start()
    {
        dmg = Boss.Instance.dmg;
        this.gameObject.SetActive(true);
        target = GameObject.FindGameObjectWithTag("Player");

        this.gameObject.GetComponent<AudioSource>().Play();

        if (target.transform.position.x > this.transform.position.x)
        {
            movedir = Vector2.right;
        }
        else
        {
            movedir = Vector2.left;
        }
    }

    private void Update()
    {
        transform.Translate(movedir * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        switch (col.tag)
        {
            case "Player":
                Debug.Log("총알 뎀 : " + dmg + " 플레이어 체력 " + PlayerManager.Instance.hp);
                if (PlayerManager.Instance.Player_god == false)
                {
                    PlayerManager.Instance.hp -= dmg;
                }
                PlayerManager.Instance.gameObject.GetComponent<AudioSource>().clip = PlayerManager.Instance.hit_sound;
                PlayerManager.Instance.gameObject.GetComponent<AudioSource>().Play();
                Destroy(this.gameObject);
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
