using UnityEngine;

public class FighterWalkState : FighterBaseState
{
    public FighterWalkState(FighterStateMachine currentContext, FighterStateFactory fighterStateFactory)
    :base(currentContext, fighterStateFactory){
        _stateName = "Walk";
    }

    public override void CheckSwitchState()
    {
        if(_ctx.Velocity.x == 0){
            SwitchState(_factory.Idle());
        }
        if(_ctx.Velocity.x < -0.5f || _ctx.Velocity.x > 0.5f){
            SwitchState(_factory.Run());
        }
        
    }

    public override void EnterState()
    {
        _ctx.Animator.SetBool("Moving", true);
    }

    public override void ExitState()
    {
        _ctx.Animator.SetBool("Moving", false);
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
        //_ctx.CharController.Move(_ctx.Velocity * _ctx.MoveSpeed);
        CheckSwitchState();
        Debug.Log("WALKING!");
        _ctx.Animator.SetFloat("Blend", _ctx.Velocity.x);
    }
}
