using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HitAdaptor : MonoBehaviour
{
    public UnityEvent<int> OnHitted;

    public void TakeDamage(int damage)
    {
        OnHitted?.Invoke(damage);
    }
}
