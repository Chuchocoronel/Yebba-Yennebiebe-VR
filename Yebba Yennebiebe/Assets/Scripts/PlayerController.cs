using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerController : MonoBehaviour
{
    public enum MagicType
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
    
    public bool leftGripTouched = false;
    public bool rightGripTouched = false;

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

    public void GripButton(MagicType type, bool isLeft)
    {
        if (isLeft)
        {
            leftDevice.TryGetFeatureValue(CommonUsages.grip, out float leftGrip);
            if (leftGrip <= 0.1f) leftGripTouched = false;
            else if (leftGrip > 0.1f && !leftGripTouched)
            {
                Debug.Log("He tocado el tipo izquierda " + type.ToString());
                leftHandMagic = type;
                leftGripTouched = true;
            }
        }
        else
        {
            rightDevice.TryGetFeatureValue(CommonUsages.grip, out float rightGrip);
            if (rightGrip <= 0.1f) rightGripTouched = false;
            else if (rightGrip > 0.1f && !rightGripTouched)
            {
                Debug.Log("He tocado el tipo derecha " + type.ToString());
                rightHandMagic = type;
                rightGripTouched = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Bottle_Endurance")
        {
            Debug.Log("Juggernaut");
            Destroy(other.gameObject);
        }
    //    if (other.name == "FireGem_Spawner")
    //    {
    //        GripButton(MagicType.FIRE);
    //    }
    //    else if (other.name == "WaterGem_Spawner")
    //    {
    //        GripButton(MagicType.WATER);
    //    }
    //    else if (other.name == "ElectricGem_Spawner")
    //    {
    //        GripButton(MagicType.ELECTRIC);
    //    }
    }
}
