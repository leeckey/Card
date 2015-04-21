using UnityEngine;
using System.Collections;

public class RoundEndHandler : BaseHandler
{
	/// <summary>
	/// 回合结束
	/// </summary>
	protected override void InitHandle()
	{
		base.InitHandle();
		
		handleList.Add(roomUI.EndRound);
	}
}
