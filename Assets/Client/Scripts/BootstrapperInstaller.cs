using Scorewarrior.Test.Data;
using Scorewarrior.Test.Models;
using Scorewarrior.Test.Models.Ui;
using Scorewarrior.Test.Views;
using UnityEngine;
using Zenject;

namespace Scorewarrior.Test
{
    public class BootstrapperInstaller : MonoInstaller
    {
        [SerializeField]
        private SceneData _sceneData;

        [SerializeField]
        private Configuration _configuration;
        
        public override void InstallBindings()
        {
            BindUtils();
            BindCharacters();
            BindWeapons();
            BindBullets();
            BindUi();

            StartGame();
        }

        private void BindUtils()
        {
            Container.Bind<IGameTime>().To<GameTime>().AsSingle().NonLazy();
            Container.BindInstance(_sceneData).AsSingle().NonLazy();
            Container.BindInstance(_configuration).AsSingle().NonLazy();
        }

        private void BindCharacters()
        {
            Container.Bind<ICharacterFactory>().To<CharacterFactory>().AsSingle().NonLazy();
            Container.BindInterfacesTo<CharacterLifecycle>().AsSingle().NonLazy();
            Container.Bind<ModificationFactory<CharacterStats>>().To<CharacterModificationFactory>().AsSingle().NonLazy();
        }
        
        private void BindWeapons()
        {
            Container.Bind<IWeaponFactory>().To<WeaponFactory>().AsSingle().NonLazy();
            Container.BindInterfacesTo<WeaponLifecycle>().AsSingle().NonLazy();
            Container.Bind<ModificationFactory<WeaponStats>>().To<WeaponModificationFactory>().AsSingle().NonLazy();
        }
        
        private void BindBullets()
        {
            Container.Bind<IBulletFactory>().To<BulletFactory>().AsSingle().NonLazy();
            Container.BindInterfacesTo<BulletLifecycle>().AsSingle().NonLazy();
        }

        private void BindUi()
        {
            Container.BindInterfacesTo<BattleUiLifecycle>().AsSingle().NonLazy();
            Container.BindInterfacesTo<HealthBarLifecycle>().AsSingle().NonLazy();
        }

        private void StartGame()
        {
            Container.Bind<IBattleSpawner>().To<BattleSpawner>().AsSingle().NonLazy();
            Container.BindInterfacesTo<BattlefieldLifecycle>().AsSingle().NonLazy();
        }
    }
}