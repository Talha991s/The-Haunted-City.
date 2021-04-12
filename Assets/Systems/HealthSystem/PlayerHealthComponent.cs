using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthComponent : HealthComponent
{
    // Start is called before the first frame update
    protected void Start()
    {
        PlayerEvents.Invoke_OnPlayerHealthSet(this);
    }
}
