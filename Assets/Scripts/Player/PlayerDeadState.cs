public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player _player, PlayerStateMachine _stateMachine, string _animParamName) : base(_player, _stateMachine, _animParamName)
    {

    }

    public override void Update()
    {
        base.Update();
        player.SetZeroVelocity();
    }
}