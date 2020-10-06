using UnityEngine;
using UnityEngine.AI;

public class Dog : MonoBehaviour, IControllable
{
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private Vector3 _lastPosition;
    private float _speed;
    private bool _isBonusAcive;
    private int _cowsFollowing;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }
    private void Start() => AddThisObjectToList();
    private void Update() => _animator.SetFloat("Speed_f", _speed);
    void FixedUpdate()
    {
        _speed = Mathf.Lerp(_speed, (transform.position - _lastPosition).magnitude / Time.deltaTime, 0.75f);
        _lastPosition = transform.position;
    }
    public void Move(Vector3 moveDestination) => _navMeshAgent.SetDestination(moveDestination);
    public void AddThisObjectToList() => GameManager.Instance.AddIControllableObject(this.gameObject);
    public void FollowedByAnimal()
    {
        _cowsFollowing += 1;
        if (_cowsFollowing == 3)
            _isBonusAcive = true;
    }
    public void UnFollowedByAnimal()
    {
        _cowsFollowing -= 1;
        if (_cowsFollowing == 0 && _isBonusAcive)
        {
            GameManager.Instance.SpawnHastePowerup();
            _isBonusAcive = false;
        }
    }
}
