using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform gunPosition;

    public Transform GunPosition
    {
        get
        {
            return gunPosition;
        }
    }

}
