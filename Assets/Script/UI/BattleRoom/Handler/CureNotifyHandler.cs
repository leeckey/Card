using UnityEngine;
using System.Collections;

public class CureNotifyHandler : BaseHandler
{
	protected override void InitHandle()
	{
		handler = roomUI.ShowCure;
	}
}
