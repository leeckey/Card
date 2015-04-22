using UnityEngine;
using System.Collections;

public class DamageNotifyHandler : BaseHandler
{

	protected override void InitHandle()
	{
		base.InitHandle();
		
		handleList.Add(roomUI.ShowDamage);
	}
}
