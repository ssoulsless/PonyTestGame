using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Haste : Powerup
{
    [SerializeField]
    private float _speedModifier = 1.5f;

    private MeshRenderer _meshRenderer;
    private SphereCollider _sphereCollider;

    private void Awake()
    {
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
        _sphereCollider = gameObject.GetComponent<SphereCollider>();
    }

    public override void PickUp(GameObject gameObject) => StartCoroutine(PowerupPlayer(gameObject));

    public void OnTriggerEnter(Collider collider)
    {
        IControllable controllable = collider.gameObject.GetComponent<IControllable>();
        if (controllable != null)
            PickUp(collider.gameObject);
    }
    public override IEnumerator PowerupPlayer(GameObject gameObject)
    {
        _sphereCollider.enabled = false;
        _meshRenderer.enabled = false;
        gameObject.GetComponent<NavMeshAgent>().speed *= _speedModifier;
        yield return new WaitForSeconds(buffDuration);
        gameObject.GetComponent<NavMeshAgent>().speed /= _speedModifier;
        Destroy(this.gameObject);
    }
}
