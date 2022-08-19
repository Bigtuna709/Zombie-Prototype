using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int bulletDamage;
    private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if(damageable != null)
        {
            Debug.Log("Hit");
            damageable.IsDamaged(bulletDamage);
            Destroy(gameObject);
        }
    }
}
