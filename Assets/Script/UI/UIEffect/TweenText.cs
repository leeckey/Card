using UnityEngine;
using System.Collections;

[RequireComponent(typeof(UILabel))]
[AddComponentMenu("ATween/Tween Text")]
public class TweenText : UITweener
{
	public int from = 0;
	public int to = 0;
	
	UILabel mLabel;
	
	
	public UILabel Label {set{mLabel = value;}}
	public UILabel cachedLabel{ get { if (mLabel == null) mLabel = GetComponent<UILabel>(); return mLabel; } }
	
	[System.Obsolete("Use 'value' instead")]
	public int width { get { return this.value; } set { this.value = value; } }
	
	/// <summary>
	/// Tween's current value.
	/// </summary>
	
	public int value { get { return int.Parse(mLabel.text); } set { mLabel.text = value.ToString(); } }
	
	/// <summary>
	/// Tween the value.
	/// </summary>
	
	protected override void OnUpdate (float factor, bool isFinished)
	{
		value = Mathf.RoundToInt(from * (1f - factor) + to * factor);		
		
	}
	
	/// <summary>
	/// Start the tweening operation.
	/// </summary>
	
	static public TweenText Begin (UILabel label, float duration,int startNumber, int endNumber,float delay = 0f)
	{
		
		TweenText comp = UITweener.Begin<TweenText>(label.gameObject, duration);
		comp.Label = label;
		comp.from = startNumber;
		comp.to = endNumber;
		if(delay != 0)
			comp.delay = delay;
		if (duration <= 0f)
		{
			comp.Sample(1f, true);
			comp.enabled = false;
		}
		return comp;
	}
	
	[ContextMenu("Set 'From' to current value")]
	public override void SetStartToCurrentValue () { from = value; }
	
	[ContextMenu("Set 'To' to current value")]
	public override void SetEndToCurrentValue () { to = value; }
	
	[ContextMenu("Assume value of 'From'")]
	void SetCurrentValueToStart () { value = from; }
	
	[ContextMenu("Assume value of 'To'")]
	void SetCurrentValueToEnd () { value = to; }
}