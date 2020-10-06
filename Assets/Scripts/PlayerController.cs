using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IControllable controllable;
    public void SetCurrentControllablePlayer(GameObject playerSelected) => controllable = playerSelected.GetComponent<IControllable>();
    public void MoveSelectedPlayer(Vector3 destination)
    {
        if (controllable != null)
            controllable.Move(destination);
    }
}
