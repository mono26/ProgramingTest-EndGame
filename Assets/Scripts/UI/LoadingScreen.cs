using UnityEngine;
using UnityEngine.UI;

namespace EndGame.Test.UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField]
        private Image loadBar;

        /// <summary>
        /// Activates or deactivates the loading screen.
        /// </summary>
        /// <param name="_activate">Activate state.</param>
        public void ToggleLoadingScreen(bool _activate)
        {
            gameObject.SetActive(_activate);
        }

        /// <summary>
        /// Update the progress bar.
        /// </summary>
        /// <param name="_progress"></param>
        public void UpdateProgress(float _progress)
        {
            loadBar.fillAmount = _progress;
        }
    }
}
