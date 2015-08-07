using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject playButton;
	public GameObject playerShip;
	public GameObject enemySpawner;
	public GameObject GameOverGameObj;
	public GameObject scoreUITextGameObj;

	public int end;

	public enum GameManagerState{
		Opening,
		Gameplay,
		GameOver,
	}

	GameManagerState GMState;

	// Use this for initialization
	void Start () {
		GMState = GameManagerState.Opening;
		end = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void UpdateGameManagerState(){
		switch(GMState){
		case GameManagerState.Opening:
			GameOverGameObj.SetActive(false);
			playButton.SetActive (true);
			end = 1;
			break;
		case GameManagerState.Gameplay:
			scoreUITextGameObj.GetComponent<GameScore>().Score = 0;
			playButton.SetActive(false);
			playerShip.GetComponent<PlayerControl>().Init ();
			enemySpawner.GetComponent<EnemySpawnerController>().GameBegin();
			end = 0;
			break;
		case GameManagerState.GameOver:
			enemySpawner.GetComponent<EnemySpawnerController>().GameEnd();
			GameOverGameObj.SetActive(true);
			Invoke ("ChangeToOpeningState", 8f);
			break;
		
		}
	}

	public void SetGameManagerState(GameManagerState state){
		GMState = state;
		UpdateGameManagerState();
	}

	public void StartGamePlay(){
		GMState = GameManagerState.Gameplay;
		UpdateGameManagerState();
	}

	public void ChangeToOpeningState(){
		SetGameManagerState (GameManagerState.Opening);
	}
}
