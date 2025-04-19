using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] SpriteRenderer _characterBody;
    [SerializeField] Animator _animator;
    [SerializeField] float _movementSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        PlayerMovement();
    }

    private void PlayerMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical"); ;

        Vector2 movement = new Vector2(moveHorizontal, moveVertical);
        movement = Vector2.ClampMagnitude(movement, 1.0f);
        _rb.linearVelocity = movement * _movementSpeed;

        bool isWalking = movement.magnitude > 0f;
        _animator.SetBool("isWalking", isWalking);

        if (isWalking) {
            bool flipSprite = movement.x < 0f;
            _characterBody.flipX = flipSprite;
        }

    }
}
