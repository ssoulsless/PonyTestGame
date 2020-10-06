using UnityEngine;
public interface IControllable
{
    void FollowedByAnimal();
    void UnFollowedByAnimal();
    void AddThisObjectToList();
    void Move(Vector3 moveDestination);
}
