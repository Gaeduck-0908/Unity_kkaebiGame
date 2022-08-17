using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//네임스페이스
namespace lib
{
    //싱글톤 디자인 패턴
    /*
    1. 게임 시스템에서?전체를 관장하는 스크립트(단일?시스템 자원 관리 차원)
    2. 게임 시스템상 전역 변수의 역할을 하는 스크립트
    3. 씬 로드시?데이터가 파괴되지 않고?유지
    4. 여러 오브젝트가 접근을 해야 하는 스크립트의 역할
    5. 단 한개의 객체만 존재
    */

    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour //제네릭 T 형식 제약 MonoBehaviour
    {
        private static T _instance; //_instance 변수

        public static T Instance
        {
            get
            {
                //instance != null 일시 return
                if (_instance != null)
                {
                    return _instance;
                }

                // 싱글톤 여부 찾음
                _instance = FindObjectOfType(typeof(T)) as T;

                //없을시
                if (_instance == null)
                {
                    //컴포넌트 가져옴
                    _instance = new GameObject(typeof(T).ToString()).AddComponent<T>();
                    DontDestroyOnLoad(_instance.gameObject);
                }

                //리턴
                return _instance;
            }
        }
    }
}