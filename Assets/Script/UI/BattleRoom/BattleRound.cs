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
	public IEnumerator AddRound(BaseAction action)
	{
		RoundStartAction roundStartAction = action as RoundStartAction;

		roundLabel.text = roundStartAction.round.ToString();

		yield return new WaitForSeconds(BattleTime.ROUND_CHANGE_TIME);
	}
}
