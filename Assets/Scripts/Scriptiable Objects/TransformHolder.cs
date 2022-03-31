using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TAK
{
    [CreateAssetMenu(menuName = "Editor/Attributes/Transform")]
    public class TransformHolder : ScriptableObject
    {
       public  Transform transformTarget;
    }
}
