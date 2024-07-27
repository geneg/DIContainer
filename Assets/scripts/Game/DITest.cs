using System;
using com.fourcatsgames.DI;
using Game.Configs;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game
{
	public class ConfigService
	{
		private GameConfig _gameConfig;

		public GameConfig GameConfig => _gameConfig;

		public ConfigService()
		{
			LoadAll();
		}
		
		private void LoadAll()
		{
			_gameConfig = Resources.Load<GameConfig>("Configs/GameUIConfig");
		}
	}
	
	 public class UIFactory
	 {
		 private readonly UIPanel _uiPanel;
		 public UIFactory(GameConfig gameConfig)
		 {
			 _uiPanel = gameConfig.UIPanel;
		 }
		 
		 public UIPanel CreatePanel(Transform transform)
		 {
			 return Object.Instantiate(_uiPanel,transform);
		 }
	 }
	
	
	public class DITest : MonoBehaviour
	{
		private DIContainer appContainer = new DIContainer();

		private void Awake()
		{
			appContainer.RegisterSingle<ConfigService>(_ => new ConfigService());
			appContainer.RegisterWithNew<UIFactory>(c => new UIFactory(c.Resolve<ConfigService>().GameConfig));
		}
		
		private void Start()
		{
			UIFactory uiFactory = appContainer.Resolve<UIFactory>();
			uiFactory.CreatePanel(this.transform);
		}

	}
}
