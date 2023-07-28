using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class CheckPlayerInFOVRange : Node
{
	private Transform transform;
	private float fovRange;

	public CheckPlayerInFOVRange(Transform transform, float range)
	{
		this.transform = transform;
		this.fovRange = range;
	}

	public override NodeState Evaluate()
	{
		if (Vector3.Distance(transform.position, ThirdPersonShooterController.Instance.transform.position) < fovRange)
		{
			state = NodeState.SUCCESS;
			return state;
		}

		state = NodeState.FAILURE;
		return state;
	}
}