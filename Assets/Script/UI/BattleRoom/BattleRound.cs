using UnityEngine;
using System.Collections;

/// <summary>
/// 战斗回合数显示
/// </summary>
public class BattleRound : MonoBehaviour
{
	public UILabel roundLabel;

	/// <summary>
	/// 回合数增加
	/// </summary>
	public float AddRound(BaseAction action)
	{
		RoundStartAction roundStartAction = action as RoundStartAction;

		roundLabel.text = roundStartAction.round.ToString();

		return 1f;
	}
}
