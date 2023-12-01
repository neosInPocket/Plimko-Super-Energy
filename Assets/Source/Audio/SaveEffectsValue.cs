using UnityEngine;

public class SaveEffectsValue : MonoBehaviour
{
	public void Save(float value)
	{
		SerializedData.Effects = value;
	}
}
