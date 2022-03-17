using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FieldofView))]
public class AudibleSensor : MonoBehaviour
{
    FieldofView fov;
    // Start is called before the first frame update
    void Start()
    {
        fov = GetComponent<FieldofView>();
        HearingManager.Instance.Register(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if(HearingManager.Instance != null)
            HearingManager.Instance.Deregister(this);
    }
    public void OnHeardSound(Vector3 location, EHeardSoundCategory category, float intensity)
    {
        if (Vector3.Distance(location, fov.transform.position) > fov.radius)
            return;

        fov.ReportCanHear(location, category, intensity);
    }

    
}
