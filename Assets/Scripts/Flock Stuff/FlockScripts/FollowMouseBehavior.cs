using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/FollowMouseBehavior")]
public class FollowMouseBehavior : FlockBehavior
{
    public Vector2 mousePosition;
    public Vector2 centerOffset;
    public float radius = 5f;

    public override Vector2 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 centerOffset = mousePosition - (Vector2)agent.transform.position;
        float t = centerOffset.magnitude / radius;

        if (t < 0.9f)
        {
            return Vector2.zero;
        }

        return centerOffset * t * t;
    }
}
