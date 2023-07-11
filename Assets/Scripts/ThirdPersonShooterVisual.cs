using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonShooterVisual : MonoBehaviour
{
	private const string SHOOT = "Shoot";

	[SerializeField] private ThirdPersonShooterController thirdPersonShooterController;

	private Animator animator;

	private void Awake()
	{
		animator = GetComponent<Animator>();
	}
}
