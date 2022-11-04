using UnityEngine;

public abstract class RoleTrait : ScriptableObject
{
    public abstract void Brain(PlayerController role);
    public virtual void Initialize(PlayerController role) { }
}
