using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarController : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;


    void RemoveTop()
    {
        Destroy(skinnedMeshRenderer.material.parent);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RemoveTop();
        }
    }
}
