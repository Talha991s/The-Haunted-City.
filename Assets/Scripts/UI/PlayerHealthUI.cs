using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private TMP_Text HealthText;

    private HealthComponent HealthComponent;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerEvents.OnPlayerHealthSet += OnPlayerHealthSet;        
    }

    private void OnPlayerHealthSet(HealthComponent healthComponent)
    {
        HealthComponent = healthComponent;
    }

    // Update is called once per frame
    void Update()
    {
        if (HealthComponent)
        {
            HealthText.text = HealthComponent.Health.ToString();
        }
    }
}
