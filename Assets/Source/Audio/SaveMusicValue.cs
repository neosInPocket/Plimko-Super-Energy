using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMusicValue : MonoBehaviour
{
    public void Save(float value)
	{
		SerializedData.Music = value;
	}
}
