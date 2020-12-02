using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/FollowMouseBehavior")]
public class FollowMouseBehavior : FilteredFlockBehavior
{
    public Vector2 center;
    public float radius = 15f;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector2 centerOffset = center - (Vector2)agent.transform.position;

        return centerOffset;
    }
}
