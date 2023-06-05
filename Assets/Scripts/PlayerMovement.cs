using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	private GameManager gameManager;

	[Header("References")]
	private Rigidbody2D rb;

	[Header("Movement")]
	public float moveSpeed;
	public float acceleration;
	public float decceleration;
	public float velPower;
	[Range(0, 1)]
	public float airControlMultiplier;
	[Space(10)]
	public float frictionAmount;

	[Header("Jump")]
	public float jumpForce;
	[Range(0, 1)]
	public float jumpCutMultiplier;
	[Space(10)]
	public float jumpMemoTime;
	private float lastJumpTime;
	[Space(10)]
	public float fallGravityMultiplier;
	private float gravityScale;
	[Space(10)]
	private bool isJumping;

	[Header("Checks")]
	public Transform groundCheckPoint;
	public Vector2 groundCheckSize;
	[Space(10)]
	public LayerMask groundLayer;

	private float horizontalInput;
	private bool flip;

	private bool grounded;

	public float knockBackTime;
	private float lastKnockBack;

	private int nbFeatherGoldUsed = 0;

	public GameObject particleJump;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		gravityScale = rb.gravityScale;
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	private void Update()
	{
		// CHECKS
		grounded = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer);
		if (rb.velocity.y < 0) isJumping = false; // If the player fall, set isJumping to false

		// RESET NB FEATHER GOLD USED
		if (grounded) nbFeatherGoldUsed = 0;

		// JUMP
		if (lastJumpTime > 0 && !isJumping && lastKnockBack <= 0)
		{
			if(grounded || nbFeatherGoldUsed < gameManager.nbFeatherGold || gameManager.nbFeatherSilver > 0)
            {
				rb.velocity *= Vector2.right;
				rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
				isJumping = true;
				lastJumpTime = 0;

				// LOOK FEATHER IF NOT GROUNDED
				if (!grounded)
                {
					Instantiate(particleJump, gameObject.transform.position, Quaternion.Euler(0, 0, 0));
					if (nbFeatherGoldUsed == gameManager.nbFeatherGold) gameManager.nbFeatherSilver--;
					else nbFeatherGoldUsed++;
				}
			}
		}

		// INPUT JUMP
		if (Input.GetKeyDown("space"))
		{
			lastJumpTime = jumpMemoTime;
		}

		if (Input.GetKeyUp("space") && isJumping)
		{
			rb.AddForce(rb.velocity.y * jumpCutMultiplier * Vector2.down, ForceMode2D.Impulse);
		}

		// TIMERS
		lastJumpTime -= Time.deltaTime;
		lastKnockBack -= Time.deltaTime;

		// SET FLIP
		horizontalInput = Input.GetAxisRaw("Horizontal");
		if (horizontalInput > 0) flip = true;
		if (horizontalInput < 0) flip = false;
	}

	void FixedUpdate()
	{
		// RUN
		var targetSpeed = horizontalInput * moveSpeed; // Calculate the direction we want to move in and our desired velocity
		var speedDif = targetSpeed - rb.velocity.x; // Calculate difference between current velocity and desired velocity
		var accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
		var tmpVelPower = grounded ? velPower : velPower * airControlMultiplier;
		var movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, tmpVelPower) * Mathf.Sign(speedDif); // Applies acceleration to speed difference, the raises to a set power so acceleration increases with higher speeds, finally multiplies by sign to reapply direction

		rb.AddForce(movement * Vector2.right); // Applies force to rigidbody, multiplying by Vector2.right so that it only affects X axis

		// FRICTION
		if (grounded && Mathf.Abs(horizontalInput) < 0.01f)
		{
			var amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));
			amount *= Mathf.Sign(rb.velocity.x);
			rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
		}

		// JUMP GRAVITY
		if (rb.velocity.y < 0) rb.gravityScale = gravityScale * fallGravityMultiplier;
		else rb.gravityScale = gravityScale;
	}

	public bool GetFlipX()
    {
		return flip;
	}

	public float GetHorizontalInput()
    {
		return horizontalInput;
    }

	public bool IsGrounded()
    {
		return grounded;
    }

	public void KnockBack(Transform attackerTransform, float power)
	{
		if (lastKnockBack > 0) return;

		var direction = ((gameObject.transform.position - attackerTransform.position) * new Vector2(1, 2)).normalized;
		rb.velocity = Vector2.zero;
		rb.AddForce(direction * power, ForceMode2D.Impulse);
		lastKnockBack = knockBackTime;
	}

}
