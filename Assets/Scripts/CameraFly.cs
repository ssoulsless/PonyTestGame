using UnityEngine;

public class CameraFly : MonoBehaviour
{
    [SerializeField]
    private float camSens = 0.25f;

    private Transform _transform;

    private void Awake() => _transform = GetComponent<Transform>();
    void Update() => _transform.Translate(Input.GetAxis("Horizontal") * camSens * Time.deltaTime, Input.GetAxis("Vertical") * camSens * Time.deltaTime, 0);
}
