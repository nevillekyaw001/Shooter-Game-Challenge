using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EnergySystem : MonoBehaviour
{
    [SerializeField] private TMP_Text energyText;
    [SerializeField] int maxEnergy;
    [SerializeField] int energyRechargeDuration;

    [HideInInspector] public int energy;

    private const string energyKey = "Energy";
    private const string energyReadyKey = "EnergyReady";

    // Start is called before the first frame update
    void Start()
    {
        energy = PlayerPrefs.GetInt(energyKey, maxEnergy);

        if(energy == 0)
        {
            string energyReadyString = PlayerPrefs.GetString(energyReadyKey, string.Empty);

            if(energyReadyString == string.Empty) { return; }

            DateTime energyReady = DateTime.Parse(energyReadyString);

            if(DateTime.Now > energyReady) //refill energy to max if Energy Recharge Duration is finished.
            {
                energy = maxEnergy;
                PlayerPrefs.SetInt(energyKey, energy);
            }
            
        }
        energyText.text = $"PLAY ({energy})";
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void DecreaseEnergy() //Reduce 1 energy and refill for 1 minute after all energy used
    {
        if (energy < 1) { return; }
        energy--;
        PlayerPrefs.SetInt(energyKey, energy);

        if (energy == 0)
        {
            DateTime energyReady = DateTime.Now.AddMinutes(energyRechargeDuration);
            PlayerPrefs.SetString(energyReadyKey, energyReady.ToString());
        }
    }

}
