using UnityEngine;

public class FighterJumpState : FighterBaseState
{
    public FighterJumpState(FighterStateMachine currentContext, FighterStateFactory fighterStateFactory)
    :base(currentContext, fighterStateFactory){
        _stateName = "Jump";
        _isRootState = true;
        InitializeSubState();
    }

    public override void CheckSwitchState()
    {
        if(_ctx.IsGrounded){
            SwitchState(_factory.Grounded());
        }
    }

    public override void EnterState()
    {
        Debug.Log("ENTERED JUMP STATE");

        // Test
        _ctx.Animator.SetTrigger("Uppercut");
    }

    public override void ExitState()
    {
        Debug.Log("EXITED JUMP STATE");
    }

    public override void FixedUpdateState()
    {
        
    }

    public override void InitializeSubState()
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }
}
