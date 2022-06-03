using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    enum MagicType
    {
        NONE = 0,
        FIRE,
        WATER,
        ELECTRIC,
        GRAVITY
    }

    MagicType leftHandMagic = MagicType.NONE;
    MagicType rightHandMagic = MagicType.NONE;
    public GemManager gemManager;
    public int hitPoints = 3;

    // Magics
    public GameObject fire;
    public GameObject water;
    public GameObject electric;
    public GameObject bullet;
    public GameObject leftHand;
    public GameObject rightHand;
    public Transform bulletSpawnPoint;

    public bool leftBackTouched = false;
    public bool rightBackTouched = false;

    InputDevice leftDevice;
    InputDevice rightDevice;

    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> leftGameControllers = new List<InputDevice>();
        InputDeviceCharacteristics leftCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftCharacteristics, leftGameControllers);

        foreach (var device in leftGameControllers)
        {
            Debug.Log(string.Format("Device name '{0}' has role '{1}'", device.name, device.role.ToString()));
        }

        leftDevice = leftGameControllers[0];

        List<InputDevice> rightGameControllers = new List<InputDevice>();
        InputDeviceCharacteristics rightCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightCharacteristics, rightGameControllers);

        rightDevice = rightGameControllers[0];
    }

    // Update is called once per frame
    void Update()
    {
        leftDevice.TryGetFeatureValue(CommonUsages.trigger, out float leftTrigger);
        if (leftTrigger <= 0.1f) leftBackTouched = false;
        else if (leftTrigger > 0.1f && !leftBackTouched)
        {
            Debug.Log("SHOOTING");
            GameObject go = Instantiate(bullet, leftHand.transform.position, Quaternion.identity);

            go.GetComponent<Rigidbody>().AddForce(leftHand.transform.forward * 1200.0f);
            leftBackTouched = true;
        }
        rightDevice.TryGetFeatureValue(CommonUsages.trigger, out float rightTrigger);
        if (rightTrigger <= 0.1f) rightBackTouched = false;
        else if (rightTrigger > 0.1f && !rightBackTouched)
        {
            Debug.Log("SHOOTING");
            GameObject go = Instantiate(bullet, rightHand.transform.position, Quaternion.identity);

            go.GetComponent<Rigidbody>().AddForce(rightHand.transform.forward * 1200.0f);
            rightBackTouched = true;
        }
    }

    public void PlayerDead()
    {
        // Do animations here, etc...
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "FireGem_Spawner")
        {
            leftHandMagic = MagicType.FIRE;
            gemManager.SpawnGem(GemManager.type.FIREGEM);
        }
        else if (other.name == "WaterGem_Spawner")
        {
            leftHandMagic = MagicType.WATER;
            gemManager.SpawnGem(GemManager.type.WATERGEM);
        }
        else if (other.name == "ElectricGem_Spawner")
        {
            leftHandMagic = MagicType.ELECTRIC;
            gemManager.SpawnGem(GemManager.type.ELECTRICGEM);
        }
    }

}
