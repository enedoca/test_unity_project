using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // �ڽ� Ŭ�������� �������� �����ϵ��� ����
    public void TakeDamage(int amount)
    {
        OnTakeDamage(amount);
    }

    // �ڽ� Ŭ������ �ݵ�� �����ؾ� ��
    protected abstract void OnTakeDamage(int amount);
}
