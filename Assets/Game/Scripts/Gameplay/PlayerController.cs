using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;

    private Transform cachedTransform;
    private Vector3 currentMovement;

    private void Awake()
    {
        cachedTransform = transform;
        currentMovement = Vector3.zero;
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        currentMovement.x = input.x;
        currentMovement.z = input.y;
        currentMovement *= speed;
    }

    private void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        cachedTransform.Translate(currentMovement * Time.deltaTime);
    }
}
