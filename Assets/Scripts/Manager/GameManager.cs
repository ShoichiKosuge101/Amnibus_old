using System;
using UnityEngine;

namespace Manager
{
    public class GameManager
        : MonoBehaviour
    {
        public GameObject playerPrefab;
        private GameObject playerInstance;

        private void Start()
        {
            GeneratePlayer();
        }

        private void GeneratePlayer()
        {
            Vector3 spawnPosition = new Vector3(1, 1, 0);
            playerInstance = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
        }
    }
}