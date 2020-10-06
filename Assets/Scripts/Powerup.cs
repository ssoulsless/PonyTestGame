using System.Collections;
using UnityEngine;

public abstract class Powerup : MonoBehaviour, IPickable
{
    public float buffDuration = 5f;
    public abstract void PickUp(GameObject gameObject);
    public abstract IEnumerator PowerupPlayer(GameObject gameObject);
}
