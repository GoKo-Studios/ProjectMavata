using UnityEngine;

public class FighterAirborneState : FighterBaseState
{   
    private Rigidbody2D _rb;

    public FighterAirborneState(FighterStateMachine currentContext, FighterStateFactory fighterStateFactory)
    :base(currentContext, fighterStateFactory){
        _stateName = "Airborne";
        _isRootState = true;
    }

    public override void CheckSwitchState()
    {
        if(_ctx.IsGrounded && !_ctx.IsJumpPressed){
            SwitchState(_factory.Grounded());
        }
    }

    public override void EnterState()
    {
        InitializeSubState();

        _rb = _ctx.Rigidbody2D;
        _ctx.Gravity = Physics2D.gravity.y;
    }

    public override void ExitState()
    {
        _ctx.Velocity = Vector2.zero;
        _rb.velocity = Vector2.zero;
    }

    public override void FixedUpdateState()
    {  
        if (_ctx.IsGravityApplied){
            float previousVelocityY = _ctx.CurrentMovement.y;
            _ctx.CurrentMovement = new Vector2(_ctx.CurrentMovement.x, _ctx.CurrentMovement.y + Physics2D.gravity.y * _ctx.GravityMultiplier * Time.fixedDeltaTime);
            _ctx.Velocity = new Vector2(_ctx.Velocity.x, Mathf.Max((previousVelocityY + _ctx.CurrentMovement.y) * .5f, -20f));    
        }
        else 
        {
            _ctx.Velocity = new Vector2(_ctx.Velocity.x, 0);
        }

        _ctx.Rigidbody2D.velocity = _ctx.Velocity;
        Debug.Log("Velocity Applied: " + _ctx.Velocity);
    }

    public override void InitializeSubState()
    {
        FighterBaseState state;
        if (_ctx.IsJumpPressed){
            state = _factory.Jump();
        }
        else{
            state = _factory.Idle();
        }

        SetSubState(state);
        state.EnterState();
    }

    public override void UpdateState()
    {
        CheckSwitchState();
    }
}