using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EHeardSoundCategory
{ ECrouch,
  EWalk,
  ESprint
};
public class HearingManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static HearingManager Instance { get; private set; } = null;

    public List<AudibleSensor> AllSensors { get; private set; } = new List<AudibleSensor>();

    void Awake()
    {
       if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Register(AudibleSensor target)
    {
        AllSensors.Add(target);
    }

    public void Deregister(AudibleSensor target)
    {
        AllSensors.Remove(target);
    }

    public void OnSoundEmitter(Vector3 location, EHeardSoundCategory category, float intesity)
    {
        foreach (var sensor in AllSensors)
        {
            sensor.OnHeardSound(location, category, intesity);
        }
    }

}
