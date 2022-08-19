using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWeapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireSpeed;

    private HandGunController handGunController;

    private void Start()
    {
        handGunController = GetComponent<HandGunController>();
    }
    public void Fire()
    {
        var bullet = Instantiate(bulletPrefab, handGunController.FireLocation.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().AddForce(transform.forward * fireSpeed , ForceMode.Impulse);
        Destroy(bullet, 3f);
    }
}
