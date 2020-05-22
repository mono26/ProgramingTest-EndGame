using EndGame.Test.Actors;
using UnityEngine;

namespace EndGame.Test.AI
{
    /// <summary>
    /// Data class that contains actor instance data for a state. Each state should have it's own data.
    /// </summary>
    public class AIStateData : ActorComponent
    {
        /// <summary>
        /// This Id is used to match datas with state. Meaning that each implementation of AIStateData matches a state.
        /// </summary>
        [SerializeField]
        private string dataId = null;

        protected virtual void Start()
        {
            GetOwner.GetComponent<AIStateController>().AddData(dataId, this);
        }
    }
}
