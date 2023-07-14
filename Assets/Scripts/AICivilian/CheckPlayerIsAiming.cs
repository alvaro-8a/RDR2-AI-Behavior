using UnityEngine;
using BehaviorTree;

public class CheckPlayerIsAiming : Node
{
	public override NodeState Evaluate()
	{
		if (ThirdPersonShooterController.Instance.isAiming)
		{
			state = NodeState.SUCCESS;
			return state;
		}

		state = NodeState.FAILURE;
		return state;
	}
}