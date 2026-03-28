using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Police : MonoBehaviour
{
    [Header("Detection Settings")]
    [SerializeField] private float detectionRadius = 25f;
    [SerializeField] private float catchRadius = 1.5f;
    [SerializeField] private Transform thief;
    [SerializeField] private LayerMask thiefLayer;

    [Header("Movement Settings")]
    [SerializeField] private float chaseSpeed = 12f;
    [SerializeField] private float patrolSpeed = 6f;

    private NavMeshAgent agent;
    private Vector3 originalPosition;
    private bool isPursuing = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        originalPosition = transform.position;
        agent.speed = patrolSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToThief = Vector3.Distance(transform.position, thief.position);
        Debug.Log($"Distance to Thief: {distanceToThief}");

        // 1. Logic for Spotting the Thief
        if (distanceToThief <= detectionRadius && CanSeeThief())
        {
            Debug.Log("Thief Spotted! Initiating Pursuit.");
            isPursuing = true;
        }

        // 2. Behavior Trees / State Logic
        if (isPursuing)
        {
            HandlePursuit(distanceToThief);
        }
        else
        {
            ReturnToOrigin();
        }
    }

    private bool CanSeeThief()
    {
        // Simple Line of Sight check
        Vector3 directionToThief = (thief.position - transform.position).normalized;
        if (Physics.Raycast(transform.position, directionToThief, out RaycastHit hit, detectionRadius))
        {
            Debug.Log($"Raycast Hit: {hit.collider.name}");
            return hit.collider.CompareTag("Player");
        }
        Debug.Log("Thief not in line of sight.");
        return false;
    }

    private void HandlePursuit(float distance)
    {
        agent.speed = chaseSpeed;
        agent.SetDestination(thief.position);

        // If the thief gets too far away, give up
        if (distance > detectionRadius * 1.5f)
        {
            isPursuing = false;
            Debug.Log("Thief escaped! Returning to post.");
        }

        // Catch logic
        if (distance <= catchRadius)
        {
            CatchThief();
        }
    }

    private void ReturnToOrigin()
    {
        agent.speed = patrolSpeed;
        agent.SetDestination(originalPosition);

        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            // Optional: Face a specific direction when home
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, Time.deltaTime * 5f);
        }
    }

    private void CatchThief()
    {
        Debug.Log("Thief Busted!");
        // Add your game over or reset logic here
        isPursuing = false; 
    }

    // Visualization in Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, catchRadius);
    }
}
