using System;
using Scorewarrior.Test.Views;
using UnityEngine.SceneManagement;
using Zenject;

namespace Scorewarrior.Test.Models.Ui
{
    public class BattleUiLifecycle : IDisposable
    {
        private BattleMenuView _menuView;
        private IBattlefieldLifecycle _battlefieldLifecycle;
        
        [Inject]
        private void Construct(SceneData sceneData, IBattlefieldLifecycle battlefieldLifecycle)
        {
            _menuView = sceneData.BattleUi.BattleMenu;
            _battlefieldLifecycle = battlefieldLifecycle;

            _battlefieldLifecycle.Ended += CheckForBattleEnd;
            _menuView.RestartButton.onClick.AddListener(ReloadScene);
            _menuView.ContinueButton.onClick.AddListener(StartBattle);
            
            _menuView.RestartButton.gameObject.SetActive(false);
            _menuView.ContinueButton.gameObject.SetActive(true);
        }
        
        public void Dispose()
        {
            _battlefieldLifecycle.Ended -= CheckForBattleEnd;
            
            _menuView.RestartButton.onClick.RemoveListener(ReloadScene);
            _menuView.ContinueButton.onClick.RemoveListener(StartBattle);
        }

        private void ReloadScene()
        {
            SceneManager.LoadScene(0);
        }

        private void StartBattle()
        {
            _menuView.ContinueButton.gameObject.SetActive(false);
            _battlefieldLifecycle.Start();
        }

        private void CheckForBattleEnd()
        {
            _menuView.RestartButton.gameObject.SetActive(true);
        }
    }
}