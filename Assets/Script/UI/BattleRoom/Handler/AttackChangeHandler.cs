using UnityEngine;
using System.Collections;

/// <summary>
/// 攻击变化处理
/// </summary>
public class AttackChangeHandler : BaseHandler
{
	protected override void InitHandle()
	{
		base.InitHandle();
		
		handleList.Add(roomUI.AttackChange);
	}
}
