using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpGun : MonoBehaviour
{
    public void PickUp(PlayerController playerController)
    {
        transform.SetParent(playerController.transform);
        transform.localPosition = new Vector3(-0.2f, 0, 0.5f);
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }
}
