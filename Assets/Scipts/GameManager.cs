

using UnityEngine;

public static class GameManager
{
    public static int currentNumberStonesThrown = 0;
    public static int score = 0;
    public static string gameMode = PlayerPrefs.GetString("Game Mode", "Generated Viruses");
    public static int numberStones = PlayerPrefs.GetInt("Number Stones", 20);
    public static int lives = PlayerPrefs.GetInt("Lives", 3);
    public static int seconds = PlayerPrefs.GetInt("Seconds", 60);
    public static float durationGame = 0;
    public static int currentSeconds = seconds;
    public static int currentLives = lives;
    public static float music = PlayerPrefs.GetFloat("Music Volume", 1);
    public static float sounds = PlayerPrefs.GetFloat("Sound Volume", 1);
}
