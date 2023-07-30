using UnityEngine;

public class AISheriffVisual : MonoBehaviour
{
	// Constant reference to animator variable
	private const string SPEED = "Speed";
	private const string MOTION_SPEED = "MotionSpeed";
	private const string SHOOT = "Shoot";

	[SerializeField] private AISheriffBT aiSheriffBT;
	[SerializeField] private AudioClip[] FootstepAudioClips;

	private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void Start()
	{
		// Event subscription
		aiSheriffBT.OnAIMoving += AISheriff_OnAIMoving;
		aiSheriffBT.OnAIStop += AISheriff_OnAIStop;
		aiSheriffBT.OnAIRun += AISheriff_OnAIRun;
		aiSheriffBT.OnAIAttack += AISheriff_OnAIAttack;
	}

	private void AISheriff_OnAIMoving(object sender, System.EventArgs e)
	{
		// Disable aiming animation
		toggleAiming(0f);

		// Set animator variables to play the Walk animation
		animator.SetFloat(SPEED, 2f);
		animator.SetFloat(MOTION_SPEED, 1f);
	}

	private void AISheriff_OnAIStop(object sender, System.EventArgs e)
	{
		// Disable aiming animation
		toggleAiming(0f);

		// Set animator variables to play the Idle animation
		animator.SetFloat(SPEED, 0f);
		animator.SetFloat(MOTION_SPEED, 0f);
	}

	private void AISheriff_OnAIRun(object sender, System.EventArgs e)
	{
		// Disable aiming animation
		toggleAiming(0f);

		// Set animator variables to play the Run animation
		animator.SetFloat(SPEED, 5f);
		animator.SetFloat(MOTION_SPEED, 1f);
	}

	private void AISheriff_OnAIAttack(object sender, System.EventArgs e)
	{
		Debug.Log("Set Aiming Animation");
		toggleAiming(1f);
	}

	private void toggleAiming(float aim)
	{
		animator.SetLayerWeight(1, aim);
	}

	private void OnFootstep(AnimationEvent animationEvent)
	{
		if (animationEvent.animatorClipInfo.weight > 0.5f)
		{
			if (FootstepAudioClips.Length > 0)
			{
				var index = Random.Range(0, FootstepAudioClips.Length);
				AudioSource.PlayClipAtPoint(FootstepAudioClips[index], transform.position, .15f);
			}
		}
	}
}