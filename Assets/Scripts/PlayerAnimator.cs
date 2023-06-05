using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public GameObject spriteGameObject;

    private PlayerMovement playerMovement;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = spriteGameObject.GetComponent<SpriteRenderer>();
        animator = spriteGameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        spriteRenderer.flipX = playerMovement.GetFlipX();
        animator.SetFloat("speed", Mathf.Abs(playerMovement.GetHorizontalInput()));
        animator.SetBool("grounded", playerMovement.IsGrounded());
    }
}
