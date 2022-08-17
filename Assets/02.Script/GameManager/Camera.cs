using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    //카메라가 추적할 오브젝트
    [SerializeField]
    GameObject pos;

    private void LateUpdate()
    {
        //카메라 이동
        this.gameObject.transform.position = new Vector3(pos.transform.position.x,0,-10);
    }
}
