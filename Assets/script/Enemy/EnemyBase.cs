using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // 자식 클래스에게 데미지를 적용하도록 위임
    public void TakeDamage(int amount)
    {
        OnTakeDamage(amount);
    }

    // 자식 클래스가 반드시 구현해야 함
    protected abstract void OnTakeDamage(int amount);
}
