using UnityEngine;

namespace EndGame.Test.Actors
{
    public class Drop : ActorComponent
    {
        [SerializeField]
        private GameObject dropPrefab;

        private bool canDrop = true;

        private void OnActorDeath(OnActorDeath _args)
        {
            if (canDrop)
            {
                if (GetOwner == _args.actor)
                {
                    CreateDrop();

                    // We only want to drop once.
                    canDrop = false;
                }
            }
        }

        private void CreateDrop()
        {
            // Instantiate the drop.
            Instantiate(dropPrefab, GetOwner.transform.position, GetOwner.transform.rotation);
        }
    }
}
