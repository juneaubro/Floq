using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Flock : MonoBehaviour
{

    public FlockAgent agentPrefab;
    public FlockBehavior behaviour;

    [Range(30, 10000)]
    public int boids = 30;
    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultipliter = 0.5f;

    private List<FlockAgent> agents = new List<FlockAgent>();
    private const float AgentDensity = 0.02f;
    private float squareMaxSpeed;
    private float squareNeighborRadius;
    private float squareAvoidanceRadius;
    
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultipliter * avoidanceRadiusMultipliter;

        for (int i = 0; i < boids; i++)
        {
            FlockAgent newAgent = Instantiate(agentPrefab, Random.insideUnitCircle * boids * AgentDensity, Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),transform);
            newAgent.name = "Agent " + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);

            Vector2 move = behaviour.CalculateMove(agent, context, this);

            move *= driveFactor;

            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }

            agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();

        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);

        foreach(Collider2D c in contextColliders)
        {
            if(c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }

        return context;
    }
}
