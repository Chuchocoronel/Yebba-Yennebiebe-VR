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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        pointsText.text = "Points: " + points;

        Color color = tpIcon.color;
        if ((playerController.teleportBonusTimer / playerController.teleportCooldown) == 1.0f)
            color.a = 1.0f;
        else
            color.a = 0.25f;
        tpIcon.color = color;

        color = blood.color;
        float lifePercent = 1.0f - (playerController.hitPoints/playerController.maxHitPoints);
        color.a = lifePercent;
        blood.color = color;
    }
}
