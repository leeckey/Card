using UnityEngine;
using System.Collections;

public class CureNotifyHandler : BaseHandler
{
	protected override void InitHandle()
	{
		base.InitHandle();
		
		handleList.Add(roomUI.CardCure);
	}
}
