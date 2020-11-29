using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController: MonoBehaviour
{
	
	public GameObject[] stones = new GameObject[3];
	public RawImage background;
	[Range (0f, 0.20f)]
	public float parallaxSpeed = 0.02f;
	
	public float torque = 5.0f;
	public float minAntiGravity = 20.0f, maxAntiGravity = 40.0f;
	public float minLateralForce = -15.0f, maxLateralForce = 15.0f;
	public float minTimeBetweenStones = 1f, maxTimeBetweenStones = 3f;
	public float minX = -30.0f, maxX = 30.0f;
	public float minZ = -5.0f, maxZ = 20.0f;
	
	private bool enableStones = true;
	private bool endGame = false;
	private Rigidbody rb;
	
	// Use this for initialization
	void Start ()
	{
		GetComponent<AudioSource>().volume = GameManager.music;
		GameManager.score = 0;
		GameManager.currentNumberStonesThrown = 0;
		GameManager.durationGame = 0;
		GameManager.currentSeconds = GameManager.seconds;
		GameManager.currentLives = GameManager.lives;
		StartCoroutine(ThrowStones());
		StartCoroutine(RefreshStatusGame());
	}
	
	// Update is called once per frame
	void Update ()
	{
		GameManager.durationGame += Time.deltaTime / 3;
		Parallax();

		// MultiTouch
		foreach (var touch in Input.touches)
		{
			if (touch.phase.Equals(TouchPhase.Began))
			{
				Ray ray = Camera.main.ScreenPointToRay(touch.position);
				if (Physics.Raycast(ray, out var hit))
				{
					GameObject gameObject = hit.transform.gameObject;
					if (gameObject.GetComponent<Stone>())
					{
						gameObject.GetComponent<Stone>().DestroyObject();
					}
				}
			}
		}
	}

	IEnumerator ThrowStones()
	{
		// Initial delay
		yield return new WaitForSeconds(2.0f);

		while(enableStones) {
			if (endGame)
			{
				parallaxSpeed = 0;
				if (GameObject.FindGameObjectsWithTag("Destroyable").Length == 0)
				{
					yield return new WaitForSeconds(4.0f);
					SceneManager.LoadScene("Final");
				}
			}
			else
			{
				GameObject stone = Instantiate(stones[Random.Range(0, stones.Length)]);
				stone.transform.position = new Vector3(Random.Range(minX, maxX), -30.0f, Random.Range(minZ, maxZ));
				stone.transform.rotation = Random.rotation;

				rb = stone.GetComponent<Rigidbody>();
			
				rb.AddTorque(Vector3.up * torque, ForceMode.Impulse);
				rb.AddTorque(Vector3.right * torque, ForceMode.Impulse);
				rb.AddTorque(Vector3.forward * torque, ForceMode.Impulse);
			
				rb.AddForce(Vector3.up * Random.Range(minAntiGravity, maxAntiGravity), ForceMode.Impulse);
				rb.AddForce(Vector3.right * Random.Range(minLateralForce, maxLateralForce), ForceMode.Impulse);
				
				GameManager.currentNumberStonesThrown++;
			}
			
			yield return new WaitForSeconds(Random.Range(minTimeBetweenStones, maxTimeBetweenStones));
		}
		
	}
	
	private void Parallax()
	{
		float finalspeed = parallaxSpeed * Time.deltaTime;
		background.uvRect = new Rect(background.uvRect.x + finalspeed, background.uvRect.y + finalspeed, 1f, 1f);
	}

	private IEnumerator RefreshStatusGame()
	{
		// Initial delay
		yield return new WaitForSeconds(2.0f);

		while (!endGame)
		{
			if (GameManager.gameMode == "Generated Viruses")
			{
				if (GameManager.currentNumberStonesThrown == GameManager.numberStones)
				{
					endGame = true;
				}
			} else if (GameManager.gameMode == "Countdown")
			{
				if (GameManager.currentSeconds == 0)
				{
					endGame = true;
				}
				else
				{
					GameManager.currentSeconds -= 1;
					yield return new WaitForSeconds(2f);
				}
			} else if (GameManager.gameMode == "Survival")
			{
				if (GameManager.currentLives == 0)
				{
					endGame = true;
				}
			}
			
			yield return new WaitForSeconds(1f);
		}
	}

}
