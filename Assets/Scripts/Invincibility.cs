using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : PowerUpBase
{
    [SerializeField] Material materialOriginal;
    [SerializeField] Material materialChangeTo;
    private Color currentColor;
    protected override void PowerUp(Player player)
    {
        currentColor = materialOriginal.color;
        Material mat = player.GetComponentInChildren<Renderer>().sharedMaterial;
        mat.color = materialChangeTo.color;
        player.setInvincible(true);
    }

    protected override void PowerDown(Player player)
    {
        Material mat = player.GetComponentInChildren<Renderer>().sharedMaterial;
        mat.color = currentColor;
        player.setInvincible(false);
    }
}
