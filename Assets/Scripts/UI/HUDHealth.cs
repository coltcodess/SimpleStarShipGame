using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StarShip
{
    public class HUDHealth : MonoBehaviour
    {
        public Image[] healthUIs;
        public Image[] powerUpUIs;
        [SerializeField] TextMeshProUGUI timerText;
        PlayerController playerController;

        private void Awake()
        {
            playerController = FindObjectOfType<PlayerController>();
        }

        private void Start()
        {
            ListenEvents();
        }

        // Update is called once per frame
        void Update()
        {

            timerText.text = "Timer: " + GameTimer.GetGameTimer().ToString();

        }

        public void ListenEvents()
        {
            playerController.HealthChangedEvent += OnHealthChanged;
            playerController.PowerUpChangedEvent += OnPowerUpChanged;
        }

        private void OnHealthChanged()
        {
            for (int i = 0; i < healthUIs.Length; i++)
            {
                if (i < playerController.health)
                {
                    healthUIs[i].enabled = true;
                }
                else
                {
                    healthUIs[i].enabled = false;
                }
            }
        }

        private void OnPowerUpChanged()
        {
            for (int i = 0; i < powerUpUIs.Length; i++)
            {
                if (i < playerController.powerUpAmount)
                {
                    powerUpUIs[i].gameObject.SetActive(true);
                }
                else
                {
                    powerUpUIs[i].gameObject.SetActive(false);
                }
            }
        }
    }
}