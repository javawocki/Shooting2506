using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���̽��� - �ش� Ŭ������ ��ӹ��� �Ļ�ũ������ �̱����� �����ϱ� ���� 
public abstract class Singletone<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Inst { get; private set; }

    //�ش� Ŭ������ ��ü�� �����ϴ� ������ ȣ��
    private void Awake()
    {
        if (Inst == null) {
            Inst = this as T;
            DoAwake();
            DontDestroyOnLoad(gameObject); //�� ����ÿ��� �ش� ������Ʈ�� �ı����� �ʵ��� ����...

        }
        else
        {
            Destroy(gameObject);
        } 
    }
    
    //�Ļ� Ŭ�������� �ڽ��� �߱�ȭ��... ó���ؾ� �ϴ� ������ ȣ���ϱ� ����...
    protected virtual void DoAwake() { 

    }
}
