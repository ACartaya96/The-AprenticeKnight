using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDisabled : EnemyBaseState
{
    //RigidbodyConstraints rb;
    float _countdown;
    float _startTime = 10.0f;

    float _rotationTime;
    // Start is called before the first frame update
    public override void EnterState(EnemyControlSystem enemy)
    {
        //rb = enemy.GetComponent<RigidbodyConstraints>();
        _countdown = _startTime;
        _rotationTime = 0;
    }
    public override void UpdateState(EnemyControlSystem enemy)
    {
        _rotationTime = Mathf.Clamp(_rotationTime + 1 * Time.deltaTime, 0, 5);
        enemy.transform.rotation = Quaternion.Lerp(enemy.transform.rotation, Quaternion.Euler(new Vector3(30, 0, 0)), _rotationTime);
        if(_countdown <= 0)
        {
            enemy.SwitchState(enemy.EnableState);
        }

        enemy.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        //enemy.enabled = false;

        _countdown = Mathf.Clamp(_countdown - 1 * Time.deltaTime, 0, _startTime);
        Debug.Log(_countdown.ToString());
    }
    public override void OnCollisionEnter(EnemyControlSystem enemy)
    {

    }
}
