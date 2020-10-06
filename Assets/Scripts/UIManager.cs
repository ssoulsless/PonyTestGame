using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    [SerializeField]
    private Text _animalsLeftText;

    [SerializeField]
    private Button _restartButton;

    private List<GameObject> _animalsLeft = new List<GameObject>();

    private void Start() => RefreshAnimalsLeftText();

    public void RefreshAnimalsLeftText()
    {
        _animalsLeft = GameManager.Instance.HerdableObjects;
        _animalsLeftText.text = "Animals left :" + _animalsLeft.Count.ToString();
    }
    public void GameEnd() => _restartButton.gameObject.SetActive(true);
}
