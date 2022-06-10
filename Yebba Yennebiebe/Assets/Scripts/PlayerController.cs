using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

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
    public float hitPoints = 3.0f;
    public float maxHitPoints = 3.0f;

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

    public bool dead = false;

    public float regenerationCooldown = 4.0f;

    public int leftShootCount = 0;
    public int rightShootCount = 0;

    // Potions and cooldowns
    private bool hasBonus = false;
    public bool juggernautActive = false;
    public float juggernautBonusTimer = 30.0f;
   
    public bool doubleTapActive = false;
    public float doubleTapBonusTimer = 30.0f;
    private bool doubleTap = false;
  
    public bool teleportActive = false;
    public float teleportBonusTimer = 30.0f;
    public float teleportCooldown = 30.0f;

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
        if (dead)
        {
            Destroy(this);
            return;
        }

        leftDevice.TryGetFeatureValue(CommonUsages.trigger, out float leftTrigger);
        if (leftTrigger <= 0.1f)
        {
            leftBackTouched = false;
            leftShootCount = 0;
        }
        else if (leftTrigger > 0.1f && !leftBackTouched)
        {
            Debug.Log("SHOOTING");
            GameObject go = Instantiate(bullet, leftHand.transform.position, Quaternion.identity);
            leftShootCount++;

            go.GetComponent<Rigidbody>().AddForce(leftHand.transform.forward * 1200.0f);
            leftBackTouched = true;
            if (doubleTap && leftShootCount < 2)
            {
                leftBackTouched = false;
            }
        }
        
        rightDevice.TryGetFeatureValue(CommonUsages.trigger, out float rightTrigger);
        if (rightTrigger <= 0.1f)
        {
            rightShootCount = 0;
            rightBackTouched = false;
        }
        else if (rightTrigger > 0.1f && !rightBackTouched)
        {
            Debug.Log("SHOOTING");
            GameObject go = Instantiate(bullet, rightHand.transform.position, Quaternion.identity);
            rightShootCount++;

            go.GetComponent<Rigidbody>().AddForce(rightHand.transform.forward * 1200.0f);
            rightBackTouched = true;
            if (doubleTap && rightShootCount < 2)
            {
                rightBackTouched = false;
            }
        }

        if (hitPoints < maxHitPoints)
        {
            regenerationCooldown -= Time.deltaTime;
            if (regenerationCooldown <= 0.0f)
            {
                hitPoints += Time.deltaTime;
                if (hitPoints >= maxHitPoints)
                {
                    hitPoints = maxHitPoints;
                }
            }
        }

        ManageCooldowns();
    }

    private void ManageCooldowns()
    {
        //if (hasBonus)
        //{
            if (juggernautActive)
            {
                if (juggernautBonusTimer <= 0.0f)
                {
                    maxHitPoints = 3.0f;
                    if (hitPoints > 3.0f)
                    {
                        hitPoints = 3.0f;
                    }
                    juggernautBonusTimer = 30.0f;
                    juggernautActive = false;
                }

                juggernautBonusTimer -= Time.deltaTime;
            }

            if (doubleTapActive)
            {
                if (doubleTapBonusTimer <= 0.0f)
                {
                    doubleTapActive = false;
                    doubleTapBonusTimer = 30.0f;
                    doubleTap = false;
                }

                doubleTapBonusTimer -= Time.deltaTime;
            }

            if (teleportActive)
            {
                if (teleportBonusTimer <= 0.0f)
                {
                    teleportActive = false;
                    teleportBonusTimer = 30.0f;
                    teleportCooldown = 30.0f;
                }

                teleportBonusTimer -= Time.deltaTime;
            }
        //}
    }
    public void PlayerDead()
    {
        // Do animations here, etc...
        dead = true;
        SceneManager.LoadScene("Lose");
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
        if (other.tag == "Juggernaut")
        {
            Debug.Log("Juggernaut");
            maxHitPoints = 5.0f;
            juggernautActive = true;
            Destroy(other.gameObject);
        }
        if (other.tag == "DoubleTap")
        {
            Debug.Log("Double Tap");
            doubleTapActive = true;
            doubleTap = true;
            Destroy(other.gameObject);
        }
        if (other.tag == "Teleport")
        {
            Debug.Log("Teleport Fast");
            teleportActive = true;
            teleportCooldown = 7.5f;
            if (teleportBonusTimer == 30.0f)
                teleportBonusTimer = 7.5f;
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
