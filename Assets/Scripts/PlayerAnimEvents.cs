using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimEvents : MonoBehaviour
{
    private PlayerScript _playerScript;
    // Start is called before the first frame update
    void Start()
    {
        _playerScript = GetComponentInParent<PlayerScript>();
    }

    // Update is called once per frame
    private void AnimationTriggers()
    {
        _playerScript.AttackAnimationFinished();
    }
}
