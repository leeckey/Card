using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// 处理动作函数委托
public delegate IEnumerator HandlerDelegate(BaseAction action);

/// <summary>
/// 行动基类
/// </summary>
public class BaseHandler : MonoBehaviour
{
	// 战斗UI
	protected BattleRoomUI roomUI;

	// 战斗控制类
	protected BattleControl battleControl;

	// 当前处理的行为
	protected BaseAction action;


	public HandlerDelegate handler;

	// 处理动画
	public virtual void Handle(BattleControl battleControl, BaseAction action)
	{
		this.battleControl = battleControl;
		roomUI = battleControl.roomUI;

		this.action = action;

		InitHandle();

		StartCoroutine(_Handle());
	}

	protected virtual void InitHandle()
	{
		handler = null;
	}

	// 处理所有动作
	protected IEnumerator _Handle()
	{
		if (handler != null)
			yield return StartCoroutine(handler(action));

		yield return new WaitForSeconds(BattleTime.ACTION_WAIT_TIME);

		battleControl.MoveNext();
	}
}
