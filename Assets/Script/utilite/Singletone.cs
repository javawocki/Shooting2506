using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//베이스스 - 해당 클랙스를 상속받은 파생크래스이 싱글톤을 적용하기 위한 
public abstract class Singletone<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Inst { get; private set; }

    //해당 클래스로 객체를 생성하는 시점에 호출
    private void Awake()
    {
        if (Inst == null) {
            Inst = this as T;
            DoAwake();
            DontDestroyOnLoad(gameObject); //씬 변경시에도 해당 오브젝트는 파괴되지 않도록 설정...

        }
        else
        {
            Destroy(gameObject);
        } 
    }
    
    //파생 클래스에서 자신의 추기화등... 처리해야 하는 로직을 호출하기 위해...
    protected virtual void DoAwake() { 

    }
}
