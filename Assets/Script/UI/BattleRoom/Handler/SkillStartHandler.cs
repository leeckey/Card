using UnityEngine;
using System.Collections;

public class SkillStartHandler : BaseHandler
{
	protected override void InitHandle()
	{
		handler = roomUI.CastSkill;
	}
}
