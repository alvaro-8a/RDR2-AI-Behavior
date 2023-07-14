using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.Animations.Rigging;

public class ThirdPersonShooterController : MonoBehaviour
{
	// Static reference
	public static ThirdPersonShooterController Instance { get; private set; }

	// ShootProjectile script
	[SerializeField] private ShootProjectile shootProjectile;
	// Virtual Camera for aiming
	[SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
	// AimRig
	[SerializeField] private Rig aimRig;
	// Sensitivity when not aiming
	[SerializeField] private float normalSensitivity;
	// Sensitivity when aiming
	[SerializeField] private float aimSensitivity;
	// LayerMask for colliders when aiming
	[SerializeField] private LayerMask aimColliderMask;
	// AimRig target position
	[SerializeField] private Transform shootPointTransform;

	// ThirdPersonController script
	private ThirdPersonController thirdPersonController;
	// Player input script
	private StarterAssetsInputs starterAssetsInputs;
	private Animator animator;
	// Controll the aim rig animation
	private float aimRigWeight;

	private void Awake()
	{
		Instance = this;
		thirdPersonController = GetComponent<ThirdPersonController>();
		starterAssetsInputs = GetComponent<StarterAssetsInputs>();
		animator = GetComponent<Animator>();
	}

	private void Update()
	{
		Vector3 inpactPoint = Vector3.zero;
		Transform hitTransform = null;

		SendRayToScreenCenter(out inpactPoint, out hitTransform);
		shootPointTransform.position = inpactPoint;

		// Check if we are aiming
		if (starterAssetsInputs.aim)
		{
			// Change Camera priority to use the aim camera
			aimVirtualCamera.Priority = 20;

			// Reduce mouse sensitivity when aiming
			thirdPersonController.SetSensitivity(aimSensitivity);
			thirdPersonController.SetRotateOnMove(false);

			// Activate the aim rig animation
			aimRigWeight = 1f;

			// Set visible the aiming animation
			animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 1f, Time.deltaTime * 15f));

			// In case we want to rotate the character to the aiming position
			// Vector3 worldAimTarget = mouseWorldPosition;
			// worldAimTarget.y = transform.position.y;
			// Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
			// transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);

			// Check if shoot input
			if (starterAssetsInputs.shoot)
			{
				shootProjectile.Shoot(inpactPoint, hitTransform);
			}
		}
		else
		{
			// Set the aim camera to a lower priority to use the main camera
			aimVirtualCamera.Priority = 5;

			// Set mouse sensitivity to normal
			thirdPersonController.SetSensitivity(normalSensitivity);
			thirdPersonController.SetRotateOnMove(true);

			// Deactivate the aim rig animation
			aimRigWeight = 0f;

			// Set invisible the aiming animation
			animator.SetLayerWeight(1, Mathf.Lerp(animator.GetLayerWeight(1), 0f, Time.deltaTime * 15f));
		}

		// Set the aim rig animation smoothly
		aimRig.weight = Mathf.Lerp(aimRig.weight, aimRigWeight, Time.deltaTime * 20f);
	}

	private void SendRayToScreenCenter(out Vector3 hitPoint, out Transform hitTransform)
	{
		Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
		Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
		if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderMask))
		{
			hitPoint = raycastHit.point;
			hitTransform = raycastHit.transform;
		}
		else
		{
			hitPoint = Vector3.zero;
			hitTransform = null;
		}
	}
}
