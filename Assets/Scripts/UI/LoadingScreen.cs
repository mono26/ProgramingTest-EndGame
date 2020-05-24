using UnityEngine;
using UnityEngine.UI;

namespace EndGame.Test.UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField]
        private Image loadBar;

        public void ToggleLoadingScreen(bool _activate)
        {
            gameObject.SetActive(_activate);
        }

        public void UpdateProgress(float _progress)
        {
            loadBar.fillAmount = _progress;
        }
    }
}
