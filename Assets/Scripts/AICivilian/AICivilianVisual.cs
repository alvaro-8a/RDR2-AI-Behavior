using UnityEngine;

public class AICivilianVisual : MonoBehaviour
{
	// Constant reference to animator variable
	private const string SPEED = "Speed";
	private const string MOTION_SPEED = "MotionSpeed";

	[SerializeField] private AICivilianBT aiCivilianBT;
	[SerializeField] private AudioClip[] FootstepAudioClips;

	private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}

	private void Start()
	{
		// Event subscription
		aiCivilianBT.OnAIMoving += AICivilian_OnAIMoving;
		aiCivilianBT.OnAIStop += AICivilian_OnAIStop;
		aiCivilianBT.OnAIEscape += AICivilian_OnAIEscape;
	}

	private void AICivilian_OnAIMoving(object sender, System.EventArgs e)
	{
		// Set animator variables to play the Walk animation
		animator.SetFloat(SPEED, 2f);
		animator.SetFloat(MOTION_SPEED, 1f);
	}

	private void AICivilian_OnAIStop(object sender, System.EventArgs e)
	{
		// Set animator variables to play the Idle animation
		animator.SetFloat(SPEED, 0f);
		animator.SetFloat(MOTION_SPEED, 0f);
	}

	private void AICivilian_OnAIEscape(object sender, System.EventArgs e)
	{
		// Set animator variables to play the Walk animation
		animator.SetFloat(SPEED, 5f);
		animator.SetFloat(MOTION_SPEED, 1f);
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