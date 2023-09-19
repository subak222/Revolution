using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private KeyCode  jumpKey = KeyCode.Space;
    private MovementRigidbody2D movement;
    private void Awake()
    {
        movement = GetComponent<MovementRigidbody2D>();
    }

    private void Update()
    {
        UpdateMove();
        UpdateJump();
    }

    private void UpdateMove()
    {
        // left, a = -1 / none = 0 / right, d = 1
        float x = Input.GetAxis("Horizontal");

        // 좌우 이동
        movement.MoveTo(x);
    }

    private void UpdateJump()
    {
        if ( Input.GetKeyDown(jumpKey) )
        {
            movement.JumpTo();
        }
        else if ( Input.GetKey(jumpKey) )
        {
            movement.IsLongJump = true;
        }
        else if ( Input.GetKeyDown(jumpKey) )
        {
            movement.IsLongJump = false;
        }
    }
}
