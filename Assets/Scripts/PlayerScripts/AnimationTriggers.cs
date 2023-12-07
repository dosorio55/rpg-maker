using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    private Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    public void AnimationFinishTrigger()
    {
        player.AnimationFinishTrigger();
    }


}
