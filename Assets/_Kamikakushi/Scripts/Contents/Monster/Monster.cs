using _Kamikakushi.Contents.Monster;
using UnityEngine;
using UnityEngine.AI;
public interface IMonsterState
{
    void Enter();
    void Update();
    void Exit();
}
public abstract class Monster : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 3.5f;
    public float giveUpPlayerDistance = 15f;
    public float waitBeforeReturnTime = 3f;
    public float returnStopDistance = 0.5f;

    protected NavMeshAgent agent;
    protected Animator animator;
    protected Detector detector;
    protected Transform player;

    public Vector3 StartPos { get; private set; }

    protected IMonsterState currentState;

    // Chase Event
    public event System.Action OnChaseStarted;
    public event System.Action OnChaseEnded;

    protected virtual void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        detector = GetComponent<Detector>();

        StartPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        agent.speed = speed;
        agent.isStopped = true;

        ChangeState(new MonsterIdleState(this));
    }

    protected virtual void Update()
    {
        currentState?.Update();
        UpdateAnimator();
    }

    public void ChangeState(IMonsterState newState)
    {
        currentState?.Exit();
        currentState = newState;
        currentState.Enter();
    }

    public void InvokeChaseStart() => OnChaseStarted?.Invoke();
    public void InvokeChaseEnd() => OnChaseEnded?.Invoke();

    protected abstract void Move(Vector3 targetPos);

    public void MoveTo(Vector3 pos)
    {
        agent.isStopped = false;
        Move(pos);
    }

    protected void UpdateAnimator()
    {
        if (animator == null) return;
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
