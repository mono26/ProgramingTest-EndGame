using EndGame.Test.Actors;
using UnityEngine;

namespace EndGame.Test.AI
{
    public class KeyRandomizer : MonoBehaviour
    {
        [SerializeField]
        private GameObject coffeeShopKey = null;
        [SerializeField]
        private Drop[] droppers = new Drop[0];

        private void Start()
        {
            PickRandomDropper();
        }

        private void PickRandomDropper()
        {
            if (droppers != null && droppers.Length > 0)
            {
                int dropper = Random.Range(0, droppers.Length);

                droppers[dropper].SetDropObject = coffeeShopKey;

                // Hide the game object.
                coffeeShopKey.SetActive(false);
            }
        }
    }
}
