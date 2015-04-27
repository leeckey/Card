using UnityEngine;
using System.Collections;

public class DamageNotifyHandler : BaseHandler
{

	protected override void InitHandle()
	{
		handler = roomUI.ShowDamage;
	}
}
