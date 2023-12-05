using UnityEngine;

public class SerializedData : MonoBehaviour
{
	[SerializeField] private bool defaultOnLoad;
	
	public static int CurrentLevelsScore
	{
		get => currentLevelsScore;
		set
		{
			currentLevelsScore = value;
			Serialize();
		}
	}
	
	public static int Coins
	{
		get => coins;
		set
		{
			coins = value;
			Serialize();
		}
	}
	
	public static int ExtraTime
	{
		get => extraTime;
		set
		{
			extraTime = value;
			Serialize();
		}
	}
	
	public static int Gravity
	{
		get => gravity;
		set
		{
			gravity = value;
			Serialize();
		}
	}
	
	public static int JustRun
	{
		get => justRun;
		set
		{
			justRun = value;
			Serialize();
		}
	}
	
	public static float Music
	{
		get => music;
		set
		{
			music = value;
			Serialize();
		}
	}
	
	public static float Effects
	{
		get => effects;
		set
		{
			effects = value;
			Serialize();
		}
	}
	
	private static int currentLevelsScore;
	private static int coins;
	private static int extraTime;
	private static int gravity;
	private static int justRun;
	private static float music;
	private static float effects;
	
	
	private void Awake()
	{
		if (defaultOnLoad)
		{
			ClearData();
		}
		
		Deserialize();
	}
	
	private static void Serialize()
	{
		PlayerPrefs.SetInt("CurrentLevelsScore", currentLevelsScore);
		PlayerPrefs.SetInt("Coins", coins);
		PlayerPrefs.SetInt("ExtraTime", extraTime);
		PlayerPrefs.SetInt("Gravity", gravity);
		PlayerPrefs.SetFloat("Music", music);
		PlayerPrefs.SetFloat("Effects", effects);
		PlayerPrefs.SetInt("JustRun", justRun);
		
		PlayerPrefs.Save();
	}
	
	private void Deserialize()
	{
		coins = PlayerPrefs.GetInt("Coins", 100);
		currentLevelsScore = PlayerPrefs.GetInt("CurrentLevelsScore", 1);
		extraTime = PlayerPrefs.GetInt("ExtraTime", 0);
		gravity = PlayerPrefs.GetInt("Gravity", 0);
		music = PlayerPrefs.GetFloat("Music", 1f);
		effects = PlayerPrefs.GetFloat("Effects", 1f);
		justRun = PlayerPrefs.GetInt("JustRun", 1);
	}

	public static void ClearData()
	{
		currentLevelsScore = 1;
		coins = 100;
		extraTime = 0;
		gravity = 0;
		music = 1f;
		effects = 1f;
		justRun = 1;
		Serialize();
	}
}
