using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Enemy : IDamagable
{
    // Start is called before the first frame update
    GameObject player { get; }

    bool isNearPlayer();

}
