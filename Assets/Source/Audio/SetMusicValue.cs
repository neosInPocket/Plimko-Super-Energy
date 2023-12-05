using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMusicValue : MonoBehaviour
{
	[SerializeField] private AudioSource audioSource;
	
	private void Start()
	{
		audioSource.volume = SerializedData.Music;
	}
	
	public void Set(float value)
	{
		audioSource.volume = value;
	}
}
