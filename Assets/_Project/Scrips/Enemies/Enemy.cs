using UnityEngine;
using Pathfinding;
using System.Runtime.InteropServices;


public class Enemy : MonoBehaviour
{
    public FollowerEntity m_AIPath;
    private Animator m_animator;
    public float naturalSpeed = 3;
    public float velocitySmoothing = 1;

    Vector2 smoothedVelocity;

    [Header("Patrol Waypoints")]
    //[SerializeField] protected PatrolPath m_patrolPath;

    [Space(10)]
    [Header("AI Target to Follow")]
    [SerializeField] public Transform m_followTarget;

    private void Awake()
    {
        InitializeComponents();

        if (m_followTarget == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                m_followTarget = player.transform;
            }
            else
            {
                Debug.LogError("Player not found in the scene.");
            }
        }

    }

    protected virtual void Start()
    {

    }
    protected virtual void Update()
    {
        var desiredVelocity = naturalSpeed > 0 ? m_AIPath.desiredVelocity / naturalSpeed : m_AIPath.desiredVelocity;
        var movementPlane = m_AIPath.movementPlane;
        var desiredVelocity2D = (Vector2)movementPlane.ToPlane(desiredVelocity, out var _);
        m_animator.SetFloat("NormalizedSpeed", m_AIPath.reachedEndOfPath || desiredVelocity2D.magnitude < 0.1f ? 0f : desiredVelocity2D.magnitude);

        smoothedVelocity = Vector3.Lerp(smoothedVelocity, desiredVelocity2D, velocitySmoothing > 0 ? Time.deltaTime / velocitySmoothing : 1);
        if (smoothedVelocity.magnitude < 0.4f)
        {
            smoothedVelocity = smoothedVelocity.normalized * 0.4f;
        }

        m_AIPath.destination = m_followTarget.position;
    }

    private void InitializeComponents()
    {
        m_animator = GetComponentInChildren<Animator>();
        m_AIPath = GetComponent<FollowerEntity>();
    }

    public Animator GetAnimator()
    {
        return m_animator;
    }


}