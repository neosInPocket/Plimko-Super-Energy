using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : Pausable
{
	[SerializeField] private Vector2 borders;
	[SerializeField] private float delay;
	[SerializeField] private JugglingBall fallingBallPrefab;
	[SerializeField] private bool Enabled; 
	private Vector2 screenSize;
	private Vector2 worldBorders;
	private bool isSpawning;
	private List<JugglingBall> jugglingBalls;
		
	private void Start()
	{
		jugglingBalls = new List<JugglingBall>();
		screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
		worldBorders = new Vector2(2 * screenSize.x * borders.x - screenSize.x, 2 * screenSize.x * borders.y - screenSize.x);
	}
	
	private void FixedUpdate()
	{
		if (isSpawning || !Enabled) return;
		StartCoroutine(Spawn());
	}
	
	public override void Enable()
	{
		Enabled = true;
	}
	
	public override void Disable()
	{
		Enabled = false;
		isSpawning = false;
	}
	
	public void Clear()
	{
		StopAllCoroutines();
		isSpawning = false;
		ClearAllBalls();
	}

	public override void Reset()
	{
		Clear();
		Disable();
	}
	
	public override void Pause()
	{
		Disable();
		foreach (var ball in jugglingBalls)
		{
			ball.Freeze();
		}
	}

	public override void UnPause()
	{
		Enable();
		foreach (var ball in jugglingBalls)
		{
			ball.UnFreeze();
		}
	}

	private void ClearAllBalls()
	{
		foreach (Transform ball in transform)
		{
			Destroy(ball.gameObject);
		}
	}
	
	private IEnumerator Spawn()
	{
		isSpawning = true;
		Vector2 position = new Vector2(Random.Range(worldBorders.x, worldBorders.y), transform.position.y);
		var ball = Instantiate(fallingBallPrefab, position, Quaternion.identity, transform);
		jugglingBalls.Add(ball);
		ball.Destroyed += OnBallDestroyed;
		yield return new WaitForSeconds(delay);
		isSpawning = false;
	}
	
	private void OnBallDestroyed(JugglingBall ball)
	{
		ball.Destroyed -= OnBallDestroyed;
		jugglingBalls.Remove(ball);
	}
}
