using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class PauseUI : MonoBehaviour
{
    public Image background;
    public Text pointsText;
    public GameObject player;
    public InputDevice leftDevice;

    // Start is called before the first frame update
    void Start()
    {
        background.gameObject.SetActive(false);

        List<InputDevice> leftGameControllers = new List<InputDevice>();
        InputDeviceCharacteristics leftCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftCharacteristics, leftGameControllers);
        leftDevice = leftGameControllers[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 vec = new Vector3(player.transform.position.x + 15, player.transform.position.y + 4, player.transform.position.z);
        gameObject.transform.localPosition = vec;

        leftDevice.IsPressed(InputHelpers.Button.MenuButton, out bool isPressed);
        if (isPressed)
        {
            Time.timeScale = 0.0f;
            background.gameObject.SetActive(true);
            pointsText.text = "Total Points: " + ScorePoints.totalPoints;
        }
    }
}
