using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerRunSpeed;
    [SerializeField] float playerWalkSpeed;
    [SerializeField] Rig rigOne;
    [SerializeField] Rig rigTwo;
    private float playerMovementSpeed;
    private Vector3 moveVelocity;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }
    private void Update()
    {
        FaceMousePosition();
        MovingControl();
        PlayWalkingAnimation();
    }
    private void MovingControl()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        moveVelocity = new Vector3(horizontal, 0f, vertical);
        if(moveVelocity.magnitude > 0)
        {
            moveVelocity.Normalize();
            moveVelocity *= playerMovementSpeed * Time.deltaTime;
            transform.Translate(moveVelocity, Space.World);
        }
    }
    private void FaceMousePosition()
    {
        Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if(groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }
    private void PlayWalkingAnimation()
    {
        if (Input.GetMouseButton(1))
        {
            if(!_animator.GetBool("IsAiming"))
            _animator.SetBool("IsAiming", true);

            playerMovementSpeed = playerWalkSpeed;
            rigOne.weight = Mathf.Lerp(rigOne.weight, 0, Time.deltaTime * 10f);
            rigTwo.weight = Mathf.Lerp(rigTwo.weight, 1, Time.deltaTime * 10f);
            float velocityZ = Vector3.Dot(moveVelocity.normalized, transform.forward);
            float velocityX = Vector3.Dot(moveVelocity.normalized, transform.right);

            _animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
            _animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
        }
        else
        {
            playerMovementSpeed = playerRunSpeed;
            rigOne.weight = Mathf.Lerp(rigOne.weight, 1, Time.deltaTime * 10f);
            rigTwo.weight = Mathf.Lerp(rigTwo.weight, 0, Time.deltaTime * 10f);
            _animator.SetBool("IsAiming", false);
            float velocityZ = Vector3.Dot(moveVelocity.normalized, transform.forward);
            float velocityX = Vector3.Dot(moveVelocity.normalized, transform.right);

            _animator.SetFloat("VelocityZ", velocityZ, 0.1f, Time.deltaTime);
            _animator.SetFloat("VelocityX", velocityX, 0.1f, Time.deltaTime);
        }
    }
}
