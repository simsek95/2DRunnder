using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeelManager : MonoBehaviour
{
    [SerializeField] MMF_Player smallCameraShake;
    [SerializeField] MMF_Player bigCameraShake;

    public static FeelManager instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else return;
    
    }


    public void SmallCameraShake()
    {
        smallCameraShake.PlayFeedbacks();
    }

    public void BigCameraShake()
    {
        bigCameraShake.PlayFeedbacks();
    }

}
