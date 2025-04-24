using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] SpriteRenderer _characterBody;
    [SerializeField] Animator _animator;
    [SerializeField] AudioClip _footstep;
    [SerializeField] float _movementSpeed;
    float _nextFootstepAudio = 0f;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        PlayerMovement();
    }

    void PlayerMovement()
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
            HandleFootsteps();
        }
    }

    void HandleFootsteps()
    {
        if (Time.time >= _nextFootstepAudio)
        {
            float frequency = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length / 2f;
            _nextFootstepAudio = Time.time + frequency;
            AudioManager.Instance.PlayAudio(_footstep, AudioManager.SoundType.SFX, 1f, false);
        }
    }
}
