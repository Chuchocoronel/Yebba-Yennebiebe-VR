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
        ICE,
        ELECTRIC,
        GRAVITY
    }

    MagicType leftHandMagic = MagicType.NONE;
    MagicType rightHandMagic = MagicType.NONE;
    public GemManager gemManager;
    public float hitPoints = 3.0f;
    public float maxHitPoints = 3.0f;

    // Magics
    public GameObject fireSpawn;
    public GameObject iceSpawn;
    public GameObject electricSpawn;

    public GameObject fire;
    public GameObject ice;
    public GameObject electric;
    public GameObject leftHand;
    public GameObject rightHand;
   
    public GameObject leftMagicSpawner;
    public GameObject rightMagicSpawner;

    public GameObject leftMagic;
    public GameObject rightMagic;

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

    public AudioSource source;
    public AudioClip[] hurtSounds;

    // Start is called before the first frame update
    void Start()
    {
        leftHandMagic = MagicType.ICE;
        rightHandMagic = MagicType.FIRE;

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

        if (leftMagicSpawner == null && rightMagicSpawner == null)
        {
            leftMagicSpawner = GameObject.Find("LeftSpawner");
            rightMagicSpawner = GameObject.Find("RightSpawner");

            leftMagic = SpawnMagic(leftHandMagic, leftMagicSpawner.transform);
            leftMagic.transform.parent = leftMagicSpawner.transform;
            rightMagic = SpawnMagic(rightHandMagic, rightMagicSpawner.transform);
            rightMagic.transform.parent = rightMagicSpawner.transform;
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
            GameObject go = ThrowMagic(leftHandMagic, leftMagicSpawner.transform);
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
            GameObject go = ThrowMagic(rightHandMagic, rightMagicSpawner.transform);
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

    public GameObject SpawnMagic(MagicType type, Transform transform)
    {
        switch (type)
        {
            case MagicType.FIRE:
                return Instantiate(fireSpawn, transform.position, Quaternion.identity);
            case MagicType.ICE:
                return Instantiate(iceSpawn, transform.position, Quaternion.identity);
            case MagicType.ELECTRIC:
                return Instantiate(electricSpawn, transform.position, Quaternion.identity);
        }

        return null;
    }

    public GameObject ThrowMagic(MagicType type, Transform transform)
    {
        switch(type)
        {
            case MagicType.FIRE:
                return Instantiate(fire, transform.position, Quaternion.identity);
            case MagicType.ICE:
                return Instantiate(ice, transform.position, Quaternion.identity);
            case MagicType.ELECTRIC:
                return Instantiate(electric, transform.position, Quaternion.identity);
        }

        return null;
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
                Destroy(leftMagic);
                leftMagic = SpawnMagic(leftHandMagic, leftMagicSpawner.transform);
                leftMagic.transform.parent = leftMagicSpawner.transform;
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
                Destroy(rightMagic);
                rightMagic = SpawnMagic(rightHandMagic, rightMagicSpawner.transform);
                rightMagic.transform.parent = rightMagicSpawner.transform;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Juggernaut")
        {
            Debug.Log("Juggernaut");
            maxHitPoints = 5.0f;
            hitPoints = 5.0f;
            juggernautActive = true;
            juggernautBonusTimer = 30.0f;
            Destroy(other.gameObject);
        }
        if (other.tag == "DoubleTap")
        {
            Debug.Log("Double Tap");
            doubleTapActive = true;
            doubleTap = true;
            doubleTapBonusTimer = 30.0f;
            Destroy(other.gameObject);
        }
        if (other.tag == "Teleport")
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            for (int i = 0; i < enemies.Length; ++i)
            {
                enemies[i].GetComponent<GroundEnemyBehaviour>().Die("Default");
            }
            Destroy(other.gameObject);
        }
    }

    public void GetHurt(float damage)
    {
        source.clip = hurtSounds[Random.Range(0, 1)];
        source.Play();
        hitPoints -= damage;
        regenerationCooldown = 4.0f;
        if (!dead && hitPoints <= 0)
        {
            source.clip = hurtSounds[2];
            source.Play();
            PlayerDead();
        }
    }
}
