using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public int points = 0;
    public Text pointsText;
    public Image blood;
    public Image tpIcon;
    public PlayerController playerController;

    public Text[] cooldownText;
    public Image[] background;
    public Image[] icon;

    // Start is called before the first frame update
    void Start()
    {
        for (int index = 0; index < 3; index++)
        {
            Color color = cooldownText[index].color;
            color.a = 0.0f;
            cooldownText[index].color = color;

            color = background[index].color;
            color.a = 0.0f;
            background[index].color = color;

            color = icon[index].color;
            color.a = 0.0f;
            icon[index].color = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // POINTS
        pointsText.text = "Points: " + points;

        // TP ICON
        Color color = tpIcon.color;
        //if (playerController.teleportBonusTimer == playerController.teleportCooldown)
        //    color.a = 1.0f;
        //else
        //    color.a = 0.25f;
        //tpIcon.color = color;

        // BLOOD
        color = blood.color;
        float lifePercent = 1.0f - (playerController.hitPoints / playerController.maxHitPoints);
        color.a = lifePercent;
        blood.color = color;

        // BONUSES
        bonusesUI(color);
    }

    private void bonusesUI(Color color)
    {
        if (playerController.doubleTapActive)
        {
            int index = 0;

            color = cooldownText[index].color;
            color.a = 1.0f;
            cooldownText[index].color = color;
            cooldownText[index].text = ((int)playerController.doubleTapBonusTimer).ToString() + "s";

            color = background[index].color;
            color.a = 1.0f;
            background[index].color = color;

            color = icon[index].color;
            color.a = 1.0f;
            icon[index].color = color;

            if (playerController.doubleTapBonusTimer <= 0)
            {
                color = cooldownText[index].color;
                color.a = 0.0f;
                cooldownText[index].color = color;

                color = background[index].color;
                color.a = 0.0f;
                background[index].color = color;

                color = icon[index].color;
                color.a = 0.0f;
                icon[index].color = color;
            }
        }

        if (playerController.juggernautActive)
        {
            int index = 1;

            color = cooldownText[index].color;
            color.a = 1.0f;
            cooldownText[index].color = color;
            cooldownText[index].text = ((int)playerController.juggernautBonusTimer).ToString() + "s";

            color = background[index].color;
            color.a = 1.0f;
            background[index].color = color;

            color = icon[index].color;
            color.a = 1.0f;
            icon[index].color = color;

            if (playerController.juggernautBonusTimer <= 0)
            {
                color = cooldownText[index].color;
                color.a = 0.0f;
                cooldownText[index].color = color;

                color = background[index].color;
                color.a = 0.0f;
                background[index].color = color;

                color = icon[index].color;
                color.a = 0.0f;
                icon[index].color = color;
            }
        }

        if (playerController.teleportActive)
        {
            int index = 2;

            color = cooldownText[index].color;
            color.a = 1.0f;
            cooldownText[index].color = color;
            cooldownText[index].text = ((int)playerController.teleportBonusTimer).ToString() + "s";

            color = background[index].color;
            color.a = 1.0f;
            background[index].color = color;

            color = icon[index].color;
            color.a = 1.0f;
            icon[index].color = color;

            if (playerController.teleportBonusTimer <= 0)
            {
                color = cooldownText[index].color;
                color.a = 0.0f;
                cooldownText[index].color = color;

                color = background[index].color;
                color.a = 0.0f;
                background[index].color = color;

                color = icon[index].color;
                color.a = 0.0f;
                icon[index].color = color;
            }
        }
    }
}
