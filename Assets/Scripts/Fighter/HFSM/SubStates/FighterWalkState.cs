using UnityEngine;

public class FighterWalkState : FighterBaseState
{
    public FighterWalkState(FighterStateMachine currentContext, FighterStateFactory fighterStateFactory)
    :base(currentContext, fighterStateFactory){
    }

    public override bool CheckSwitchState()
    {
        if (_ctx.ActionInput.Read()){
            SwitchState(_factory.GetSubState(FighterSubStates.Attack));
            return true;
        }
        else if (_ctx.DodgeInput.Read()){
            SwitchState(_factory.GetSubState(FighterSubStates.Dodge));
            return true;
        }
        else if (_ctx.DashInput.Read()){
            SwitchState(_factory.GetSubState(FighterSubStates.Dash));
            return true;
        }
        else if(_ctx.MovementInput.Read() == 0){
            if(IdleStateSwitchCheck()) return true; 
            
            SwitchState(_factory.GetSubState(FighterSubStates.Idle));
            return true;
        }
        else return false;
    }

    public override void EnterState()
    {   
        _ctx.Drag = 0f;
        
        if (_ctx.IsGrounded)
        {
            _ctx.Animator.PlayInFixedTime("MoveBT");
        }
    }

    public override void ExitState()
    {
        _ctx.Animator.SetFloat("Blend", 0f);
        _ctx.Gravity = 0f;
        _ctx.Drag = 0f;
        _ctx.CurrentMovement = new Vector2(0, _ctx.Velocity.y);
        _ctx.Velocity = _ctx.CurrentMovement;
        //_ctx.Rigidbody2D.velocity = _ctx.Velocity;
        _ctx.FighterController.targetVelocity = _ctx.Velocity;
    }

    public override void FixedUpdateState()
    {
        if (_ctx.CurrentRootState == FighterStates.Grounded){
            _ctx.Animator.SetFloat("Blend", _ctx.MovementInput.Read() * _ctx.FaceDirection);
            _ctx.CurrentMovement = _ctx.RootMotion;
        }
        else if (_ctx.CurrentRootState == FighterStates.Airborne)
        {
            _ctx.CurrentMovement = new Vector2(_ctx.MovementInput.Read() * _ctx.FaceDirection * _ctx.AirMoveSpeed, _ctx.CurrentMovement.y);
        }

        CheckSwitchState();
    }

    public override void InitializeSubState()
    {
    }

    public override void UpdateState()
    {
    }
}
