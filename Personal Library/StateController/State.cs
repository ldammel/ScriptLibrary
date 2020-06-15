using UnityEngine;

[CreateAssetMenu(fileName = "State", menuName = "AI/States/New State")]
public class State : ScriptableObject
{
    public Action[] actions;
    public Color sceneGizmoColor = Color.grey; 
    
    public void UpdateState(StateController controller)
    {
        DoActions(controller);
    }

    private void DoActions(StateController controller)
    {
        foreach (var t in actions)
        {
            t.Act(controller);
        }
    }
}