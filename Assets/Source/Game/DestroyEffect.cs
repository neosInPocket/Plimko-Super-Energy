using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
	
	[SerializeField] private ParticleSystem particles;
	public Action<DestroyEffect> EffectEnd;
	
	private void Start()
	{
		StartCoroutine(DeathRoutine());
	}
	
	private IEnumerator DeathRoutine()
	{
		yield return new WaitForSeconds(particles.main.duration);
		EffectEnd?.Invoke(this);
		Destroy(gameObject);
	}
}
