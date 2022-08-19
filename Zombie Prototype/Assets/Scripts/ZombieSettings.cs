using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSettings : Singleton<ZombieSettings>
{
    [SerializeField] float zombieWalkSpeed;
    public static float ZombieWalkSpeed => Instance.zombieWalkSpeed;
    [SerializeField] float zombieAggroDistance;
    public static float ZombieAggroDistance => Instance.zombieAggroDistance;
    [SerializeField] float zombieAttackSpeed;
    public static float ZombieAttackSpeeed => Instance.zombieAttackSpeed;


}
