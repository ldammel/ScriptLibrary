using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityEventCollision : UnityEvent<Collision>{}

[System.Serializable]
public class CollisionAction : InteractionAction<UnityEventCollision>{}

public class OnCollisionController : OnInteractionController<CollisionAction, UnityEventCollision>
{
    void OnCollisionEnter(Collision other)
    {
        if (actions == null)
            return;

        Collider col = other.collider;
        if (actions.ContainsKey(col.tag))
        {
            actions[col.tag].OnInteraction?.Invoke(other);
            if (printDebug)
                print("Collision " + col.name);
        }
    }
}