using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerMovementSpeed;
    private Vector3 _input;
    private Vector3 moveVelocity;
    private Rigidbody _rigidBody;

    private void Awake() => _rigidBody = GetComponent<Rigidbody>();
    private void Update()
    {
        GetMovementInputs();
        FaceMousePosition();
    }
    private void FixedUpdate() => _rigidBody.velocity = moveVelocity;
    private void GetMovementInputs()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        moveVelocity = _input * playerMovementSpeed;
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
}
