using UnityEngine;
using StarterAssets;

public class ShootProjectile : MonoBehaviour
{
	private const string SHOOT = "Shoot";

	[SerializeField] private Transform pfBulletProjectile;
	[SerializeField] private Transform spawnBulletPosition;
	[SerializeField] private ParticleSystem shotParticles;
	[SerializeField] private AudioClip shootSound;
	[SerializeField] private GameObject vfxHitGreen;
	[SerializeField] private GameObject vfxHitRed;
	[SerializeField] private float shakeIntensity = 0.5f;
	[SerializeField] private float shakeDuration = 0.1f;

	private StarterAssetsInputs starterAssetsInputs;
	private Animator animator;
	private AudioSource audioSource;

	private void Awake()
	{
		animator = GetComponent<Animator>();
		audioSource = GetComponent<AudioSource>();
		starterAssetsInputs = GetComponent<StarterAssetsInputs>();
	}

	public void Shoot(Vector3 inpactPoint, Transform hitTransform)
	{
		// Trigger Shoot animation
		animator.SetTrigger(SHOOT);

		// Play shoot sound
		audioSource.PlayOneShot(shootSound, 0.5f);

		// Emit particles
		shotParticles.Emit(15);

		// Shake camera
		CinemachineShake.Instance.ShakeCamera(shakeIntensity, shakeDuration);

		/* BULLET PREFAB */
		Vector3 aimDirection = (inpactPoint - spawnBulletPosition.position).normalized;
		Transform bullet = Instantiate(pfBulletProjectile, spawnBulletPosition.position, Quaternion.LookRotation(aimDirection, Vector3.up));
		bullet.GetComponent<BulletProjectileRaycast>().Setup(inpactPoint);

		/* RAYCAST METHOD */

		// Check if we hit something
		// if (hitTransform != null)
		// {
		// 	// Hit something
		// 	if (hitTransform.GetComponent<BulletTarget>() != null)
		// 	{
		// 		// Hit target
		// 		Instantiate(vfxHitGreen, mouseWorldPosition, Quaternion.identity);
		// 	}
		// 	else
		// 	{
		// 		// missed target
		// 		Instantiate(vfxHitRed, mouseWorldPosition, Quaternion.identity);

		// 	}
		// }

		// Make sure it doesn`t shoot constantly
		starterAssetsInputs.shoot = false;
	}

}