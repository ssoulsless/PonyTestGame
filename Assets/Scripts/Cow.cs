using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cow : MonoBehaviour, IHerdable
{
    private const float _maxHerdTime = 3f;
    private const float _minHerdTime = 7f;

    private const float _agroRange = 10f;

    private float _speed;

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private Transform _transform;
    private List<GameObject> _controllables = new List<GameObject>();
    private Transform _objectTransform;

    private Vector3 _destinationToGo;
    private Vector3 _lastPosition;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        InvokeRepeating(nameof(Tick), 0, 0.25f);
        InvokeRepeating(nameof(Herd), 0, Random.Range(_minHerdTime, _maxHerdTime));
        AddThisObjectToList();
    }
    private void Start() => _controllables = GameManager.Instance.ControllableObjects;
    private void Update() => _animator.SetFloat("Speed_f", _speed);
    private void OnTriggerEnter(Collider other)
    {
        Destination destination = other.gameObject.GetComponent<Destination>();
        if (destination != null)
            DestinationReached();
    }
    public void DestinationReached()
    {
        if (_objectTransform != null)
            _objectTransform.gameObject.GetComponent<IControllable>().UnFollowedByAnimal();
        GameManager.Instance.OnDestinationReached.Invoke(this.gameObject);
    }

    public void AddThisObjectToList() => GameManager.Instance.AddHerdableObject(this.gameObject);
    void FixedUpdate()
    {
        _speed = Mathf.Lerp(_speed, (transform.position - _lastPosition).magnitude / Time.deltaTime, 0.75f);
        _lastPosition = transform.position;
    }
    private Vector3 GetNewPositionToHerd(Vector3 currentPosition)
    {
        float newXPos = currentPosition.x + (Random.Range(-10, 10));
        float newZPos = currentPosition.z + (Random.Range(-10, 10));
        Vector3 newPos = new Vector3(newXPos, currentPosition.y, newZPos);
        return newPos;
    }
    public void Follow() => _navMeshAgent.SetDestination(_objectTransform.position);
    public void Herd() => _destinationToGo = GetNewPositionToHerd(_transform.position);
    public void Tick()
    {
        _navMeshAgent.speed = 5f;
        _navMeshAgent.destination = _destinationToGo;
        foreach (var obj in _controllables)
        {
            if (Vector3.Distance(obj.transform.position, _transform.position) <= _agroRange)
            {
                _objectTransform = obj.GetComponent<Transform>();
                _navMeshAgent.speed = 8f;
                InvokeRepeating(nameof(Follow), 0, 0.1f);
                obj.GetComponent<IControllable>().FollowedByAnimal();
                CancelInvoke(nameof(Tick));
                CancelInvoke(nameof(Herd));
            }
        }
    }
}
