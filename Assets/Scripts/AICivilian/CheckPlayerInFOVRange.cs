using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class CheckPlayerInFOVRange : Node
{
	private Transform transform;

	public CheckPlayerInFOVRange(Transform transform)
	{
		this.transform = transform;
	}

	public override NodeState Evaluate()
	{
		if (Vector3.Distance(transform.position, ThirdPersonShooterController.Instance.transform.position) < 5f)
		{
			Debug.Log("RUN AWAY!");
			state = NodeState.SUCCESS;
			return state;
		}

		state = NodeState.FAILURE;
		return state;
	}
}