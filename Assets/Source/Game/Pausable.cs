using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pausable : MonoBehaviour
{
    public abstract void Reset();
	public abstract void Enable();
	public abstract void Disable();
	public abstract void Pause();
	public abstract void UnPause();
}
