public class BaseCondition : StateTransitionCondition
{
    public override bool IsMet()
    {
        return TestCondition();
    }

    public bool TestCondition()
    {
        return true;
    }
}