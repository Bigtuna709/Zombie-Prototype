using UnityEngine;

public class HandGunController : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private Transform fireLocation;
    private PickUpGun _pickUpGun;
    private FireWeapon _fireWeapon;

    public Transform FireLocation { get { return fireLocation; } }
    private void Start()
    {
        _pickUpGun = GetComponent<PickUpGun>();
        _fireWeapon = GetComponent<FireWeapon>();
    }
    private void Update()
    {
        if(playerController != null)
        {
            if(Input.GetMouseButtonDown(0))
            {
                _fireWeapon.Fire();
            }
        }
        else
            return;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>())
        {
            var player = other.GetComponent<PlayerController>();
            playerController = player;

            _pickUpGun.PickUp(playerController);
        }
    }
}
