public interface IState
{
    IState ProcessTransitions();
    void Enter();
    void Exit();
}