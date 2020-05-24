using EndGame.Test.AI;
using EndGame.Test.Events;
using EndGame.Test.UI;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EndGame.Test.Game
{
    public class GameStateManager : Singleton<GameStateManager>
    {
        public Action<IEventArgs> OnUIButtonPressedEvent;

        [SerializeField]
        private LoadingScreen loadingScreen = null;

        private const string WIN_TEXT = "Victory!";
        private const string DEFEAT_TEXT = "Defeat.";
        private const string NO_KEY_TOOLTIP = "Get the key from one of the guards.";

        private void Start()
        {
            OnUIButtonPressedEvent = (args) => OnUIButtonPressed((OnUIButtonPressedEventArgs)args);

            EventController.SubscribeToEvent(UIEvents.BUTTON_PRESSED, OnUIButtonPressedEvent);
        }

        private void OnDestroy()
        {
            OnUIButtonPressedEvent = (args) => OnUIButtonPressed((OnUIButtonPressedEventArgs)args);

            EventController.UnSubscribeFromEvent(UIEvents.BUTTON_PRESSED, OnUIButtonPressedEvent);
        }

        private void OnUIButtonPressed(OnUIButtonPressedEventArgs _args)
        {
            switch(_args.buttonPressed)
            {
                case "playgame":
                    {
                        OnPlayGame();
                        break;
                    }
                case "mainmenu":
                    {
                        OnMainMenu();
                    }
                    break;
                case "replay":
                    {
                        OnReplayGame();
                        break;
                    }
                case "quit":
                    {
                        Application.Quit();
                        break;
                    }
            }
        }

        private void OnPlayGame()
        {
            Time.timeScale = 1.0f;

            StartCoroutine(LoadScene("GameScene"));
        }

        private IEnumerator LoadScene(string _sceneName)
        {
            loadingScreen.ToggleLoadingScreen(true);
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(_sceneName);

            while (!loadOperation.isDone)
            {
                loadingScreen.UpdateProgress(loadOperation.progress);

                yield return null;
            }

            loadingScreen.UpdateProgress(1.0f);

            yield return new WaitForSeconds(1.0f);

            loadingScreen.ToggleLoadingScreen(false);
        }

        private void OnMainMenu()
        {
            Time.timeScale = 1.0f;

            StartCoroutine(LoadScene("MainMenu"));
        }

        private void OnReplayGame()
        {
            OnPlayGame();
        }

        private void OnPlayerWon()
        {
            Time.timeScale = 0.1f;

            InGameStateMenu.ShowStateScreen(WIN_TEXT);
        }

        private void OnPlayerLoose()
        {
            Time.timeScale = 0.1f;

            InGameStateMenu.ShowStateScreen(DEFEAT_TEXT);
        }

        private void OnPlayerHasNoKey()
        {
            ToolTip.DisplayToolTip(NO_KEY_TOOLTIP);
        }
    }
}
