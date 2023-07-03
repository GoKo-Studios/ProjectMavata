using UnityEngine;

public class FighterGroundedState : FighterBaseState
{
    public FighterGroundedState(FighterStateMachine currentContext, FighterStateFactory fighterStateFactory)
    :base(currentContext, fighterStateFactory){
        _isRootState = true;
    }

    public override void CheckSwitchState()
    {
        if (_ctx.IsHurt && !_ctx.StaminaManager.CanBlock){
            SwitchState(_factory.Stunned());
        }

        if (!_ctx.IsGrounded || _ctx.IsJumpPressed){
            SwitchState(_factory.Airborne());
        }
    }

    public override void EnterState()
    {
        InitializeSubState();

        _ctx.Gravity = Physics2D.gravity.y;
        _ctx.Rigidbody2D.velocity = new Vector2(_ctx.Rigidbody2D.velocity.x, _ctx.Gravity);
    }

    public override void ExitState()
    {    
    }

    public override void FixedUpdateState()
    {
        if (_ctx.Velocity.x != 0)
        _ctx.Rigidbody2D.velocity = _ctx.Velocity;
        CheckSwitchState();
    }

    public override void InitializeSubState()
    {
        FighterBaseState state;
        if(_ctx.MovementInput == 0){
            state = _factory.Idle();
        }
        else{
            state = _factory.Walk();
        }
        SetSubState(state);
        state.EnterState();
    }

    public override void UpdateState()
    {     
    }
}
