using EndGame.Test.AI;
using EndGame.Test.Events;
using EndGame.Test.Items;
using EndGame.Test.UI;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EndGame.Test.Game
{
    public class GameStateManager : Singleton<GameStateManager>
    {
        public Action<IEventArgs> OnUIButtonPressedListener;
        public Action<IEventArgs> OnPlayerWonListener;
        public Action<IEventArgs> OnPlayerDeathListener;
        public Action<IEventArgs> OnPlayerHasNoKeyListener;
        private Action<IEventArgs> OnPickUpPickedListener = null;

        [SerializeField]
        private LoadingScreen loadingScreen = null;

        private const string WIN_TEXT = "Victory!";
        private const string DEFEAT_TEXT = "Defeat.";
        private const string NO_KEY_TOOLTIP = "Get the key from one of the guards.";
        private const string UNLOCK_COFFEE_SHOP = "Go and unlock the coffee shop";

        private void Start()
        {
            OnUIButtonPressedListener = (args) => OnUIButtonPressed((OnUIButtonPressedEventArgs)args);
            OnPlayerWonListener = (args) => OnPlayerWon((OnPlayerWonEventArgs)args);
            OnPlayerDeathListener = (args) => OnPlayerDeath((OnPlayerDeathEventArgs)args);
            OnPlayerHasNoKeyListener = (args) => OnPlayerHasNoKey((OnPlayerHasNoKeyEventArgs)args);
            OnPickUpPickedListener = (args) => OnPickUpCoffeeKey((OnPickUpCoffeeKeyEventArgs)args);

            EventController.SubscribeToEvent(UIEvents.BUTTON_PRESSED, OnUIButtonPressedListener);
            EventController.SubscribeToEvent(GameEvents.PLAYER_WON, OnPlayerWonListener);
            EventController.SubscribeToEvent(GameEvents.PLAYER_DEATH, OnPlayerDeathListener);
            EventController.SubscribeToEvent(GameEvents.PLAYER_HAS_NO_KEY, OnPlayerHasNoKeyListener);
            EventController.SubscribeToEvent(PickUpEvents.PICKUP_COFFEE_KEY, OnPickUpPickedListener);
        }

        private void OnDestroy()
        {
            OnUIButtonPressedListener = (args) => OnUIButtonPressed((OnUIButtonPressedEventArgs)args);

            EventController.UnSubscribeFromEvent(UIEvents.BUTTON_PRESSED, OnUIButtonPressedListener);
            EventController.UnSubscribeFromEvent(GameEvents.PLAYER_WON, OnPlayerWonListener);
            EventController.UnSubscribeFromEvent(GameEvents.PLAYER_DEATH, OnPlayerDeathListener);
            EventController.UnSubscribeFromEvent(GameEvents.PLAYER_HAS_NO_KEY, OnPlayerHasNoKeyListener);
            EventController.UnSubscribeFromEvent(PickUpEvents.PICKUP_PICKED, OnPickUpPickedListener);
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

        private void OnPlayerWon(OnPlayerWonEventArgs _args)
        {
            Time.timeScale = 0.0f;

            InGameStateMenu.ShowStateScreen(WIN_TEXT);
        }

        private void OnPlayerDeath(OnPlayerDeathEventArgs _args)
        {
            Time.timeScale = 0.0f;

            InGameStateMenu.ShowStateScreen(DEFEAT_TEXT);
        }

        private void OnPlayerHasNoKey(OnPlayerHasNoKeyEventArgs _args)
        {
            ToolTip.DisplayToolTip(NO_KEY_TOOLTIP);
        }

        private void OnPickUpCoffeeKey(OnPickUpCoffeeKeyEventArgs _args)
        {
            ToolTip.DisplayToolTip(UNLOCK_COFFEE_SHOP);
        }
    }
}
