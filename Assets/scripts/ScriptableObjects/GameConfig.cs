using UnityEngine;

namespace Game.Configs
{
	[CreateAssetMenu(fileName = "GameUIConfig", menuName = "GameConfigs/UIConfig", order = 1)]
	public class GameConfig : ScriptableObject
	{
		[SerializeField]
		private UIPanel _uiPanel;

		public UIPanel UIPanel => _uiPanel;
	}
}
