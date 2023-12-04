using UnityEngine;

[CreateAssetMenu(menuName = "Colors")]
public class AllColorSO : ScriptableObject
{
	[SerializeField] private Color[] colors;
	public Color[] Colors => colors;
}
