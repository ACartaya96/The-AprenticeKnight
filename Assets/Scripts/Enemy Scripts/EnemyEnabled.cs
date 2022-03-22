using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnabled : EnemyBaseState
{
    float _rotationTime;
    
    // Start is called before the first frame update
    public override void EnterState(EnemyControlSystem enemy)
    {
        _rotationTime = 0;
        
    }
    public override void UpdateState(EnemyControlSystem enemy)
    {
        enemy.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        _rotationTime = Mathf.Clamp(_rotationTime + 1 * Time.deltaTime, 0, 5);
        enemy.transform.rotation = Quaternion.Lerp(Quaternion.Euler(new Vector3(30, 0, 0)),enemy.transform.rotation, _rotationTime);

        
        Debug.Log(enemy.name + " Enabled");
        if(_rotationTime >= 5)
            enemy.SwitchState(enemy.PatrolState);
       
    }
    public override void OnCollisionEnter(EnemyControlSystem enemy)
    {

    }
}
