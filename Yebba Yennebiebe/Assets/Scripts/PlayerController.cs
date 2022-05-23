using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Input to throw magic in VR

        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    Instantiate(fire, this.transform);
        //}
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
