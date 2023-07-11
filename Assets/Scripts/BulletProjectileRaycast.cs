using UnityEngine;

public class BulletProjectileRaycast : MonoBehaviour
{

	[SerializeField] private Transform vfxHitGreen;
	[SerializeField] private Transform vfxHitRed;
	[SerializeField] private float moveSpeed = 200f;

	private Vector3 targetPosition;

	public void Setup(Vector3 targetPosition)
	{
		this.targetPosition = targetPosition;
	}

	private void Update()
	{
		float distanceBefore = Vector3.Distance(transform.position, targetPosition);

		Vector3 moveDir = (targetPosition - transform.position).normalized;
		transform.position += moveDir * moveSpeed * Time.deltaTime;

		float distanceAfter = Vector3.Distance(transform.position, targetPosition);

		if (distanceBefore < distanceAfter)
		{
			Instantiate(vfxHitRed, targetPosition, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}