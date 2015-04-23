using UnityEngine;
using System.Collections;

public class MaxHpChangeHandler : BaseHandler
{
	protected override void InitHandle()
	{
		base.InitHandle();
		
		handleList.Add(roomUI.MaxHpChange);
	}
}
