using Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Consumable", menuName = "Items/Consumable", order = 2)]
public class ConsumableScriptable : ItemScriptables
{
    public int Effect = 0;

    public override void UseItem(PlayerController controller)
    {
        if (controller.Health.Health >= controller.Health.MaxHealth) return;
        controller.Health.HealPlayer(Effect);

        SetAmount(Amount - 1);
        if (Amount <= 0)
        {
            DeleteItem(controller);
        }
    }
}
