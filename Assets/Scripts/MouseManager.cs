using System;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    private static MouseManager _instance;
    public static MouseManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("MouseManager is NULL!!!");
            return _instance;
        }
    }

    public EventVector3 OnGroundClicked;
    public EventGameObject AtPlayerClicked;

    private Camera _camera;
    private IControllable _controllable;
    private void Awake()
    {
        _instance = this;
        _camera = GetComponent<Camera>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray origin = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(origin, out RaycastHit raycastHit))
            {
                _controllable = raycastHit.collider.GetComponent<IControllable>();
                if (_controllable != null)
                    AtPlayerClicked.Invoke(raycastHit.collider.gameObject);
                else
                    OnGroundClicked.Invoke(raycastHit.point);
            }
        }
    }
}