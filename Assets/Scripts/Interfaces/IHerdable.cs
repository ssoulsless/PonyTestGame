using UnityEngine;

public interface IHerdable
{
    void DestinationReached();  
    void Follow();
    void Herd();
    void Tick();
    void AddThisObjectToList();
}
