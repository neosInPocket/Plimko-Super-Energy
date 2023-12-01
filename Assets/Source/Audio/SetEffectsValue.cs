using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEffectsValue : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
	
	private void Start()
	{
		audioSource.volume = SerializedData.Effects;
	}
}
