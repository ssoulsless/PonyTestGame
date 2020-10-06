using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("GameManager is NULL!!!");
            return _instance;
        }
    }
    [Tooltip("Put here Haste Powerup Prefab")]
    [SerializeField]
    private GameObject _hastePowerupPrefab;

    public EventGameObject OnDestinationReached;
    public UnityEvent OnGameEnded;


    private List<GameObject> _controllableObjects = new List<GameObject>();
    public List<GameObject> ControllableObjects
    {
        get
        {
            return _controllableObjects;
        }
    }


    private List<GameObject> _herdableObjects = new List<GameObject>();
    public List<GameObject> HerdableObjects
    {
        get
        {
            return _herdableObjects;
        }
    }


    private void Awake() => _instance = this;
    public void AddIControllableObject(GameObject gameObject) => _controllableObjects.Add(gameObject);
    public void AddHerdableObject(GameObject gameObject) => _herdableObjects.Add(gameObject);
    public void RestartLevel() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    public void RemoveAnimalFromList(GameObject animal)
    {
        _herdableObjects.Remove(animal);
        Destroy(animal);
        if (_herdableObjects.Count == 0)
            OnGameEnded.Invoke();
    }
    public void SpawnHastePowerup() => Instantiate(_hastePowerupPrefab, GeneratePositionForPowerup(), Quaternion.identity);
    private Vector3 GeneratePositionForPowerup() => new Vector3(UnityEngine.Random.Range(-20f, 10f), 0f, UnityEngine.Random.Range(-30f, -15f));

}
[Serializable]
public class EventVector3 : UnityEvent<Vector3> { }

[Serializable]
public class EventGameObject : UnityEvent<GameObject> { }
