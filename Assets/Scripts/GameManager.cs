using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Transform playerStart;
    public PlayerSettings playerSettings;
    public GameObject player;
    public GameObject gameCamera;

    public static GameManager GetInstance()
    {
        return TryCreateSingleton();
    }

    public static GameManager TryCreateSingleton()
    {
        if (!instance && Application.isPlaying)
        {
            var gameManagerObject = new GameObject("Game Manager");
            var gameManager = gameManagerObject.AddComponent<GameManager>(); 

            instance = gameManager;
            DontDestroyOnLoad(instance);
        }

        return instance;
    }

    public static void TryMakeSingleton(GameManager gameManager)
    {
        if (!instance)
        {
            instance = gameManager;
            DontDestroyOnLoad(instance);
            return;
        }
        else if (instance && instance != gameManager)
        {
            Destroy(instance);
        }
    }

    void Awake()
    {
        TryMakeSingleton(this);
        
        if (!playerStart)
        {
            playerStart = new GameObject("Player Start").GetComponent<Transform>();
            playerStart.parent = transform;
        }

        if (!playerSettings)
        {
            playerSettings = Resources.Load<PlayerSettings>("ScriptableObjects/DefaultPlayerSettings");
        }

        if (!playerSettings)
        {
            Debug.LogWarning("Missing ScriptableObjects/DefaultPlayerSettings ScriptableObject!");
            playerSettings = ScriptableObject.CreateInstance<PlayerSettings>();
        }

        if (!player)
        {
            var playerPrefab = Resources.Load<GameObject>("Prefabs/Player");
            
            if (playerPrefab)
            {
                player = Instantiate(playerPrefab);
                player.transform.position = playerStart.position;
            }
            else 
            {
                Debug.LogWarning("Missing Prefabs/Player");
            }
        }

        if (!gameCamera)
        {
            var gameCameraPrefab = Resources.Load<GameObject>("Prefabs/Camera");

            if (gameCameraPrefab)
            {
                gameCamera = Instantiate(gameCameraPrefab);
                var cinemachine = gameCamera?.GetComponentInChildren<CinemachineVirtualCamera>();
                var target = player?.GetComponentInChildren<CameraTarget>();

                if (cinemachine && target)
                {
                    cinemachine.m_LookAt = target.transform;
                    cinemachine.m_Follow = target.transform;
                }
            }
            else
            {
                Debug.LogWarning("Missing Prefabs/Camera");
            }
        }
    }
}
