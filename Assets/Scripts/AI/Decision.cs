using UnityEngine;

public abstract class Decision : ScriptableObject
{
    public abstract bool Decide(AIStateController _controller, AIStateData _data);
}
