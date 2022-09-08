using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]//이 스크립트를 가진 오브젝트는 반드시 Animator 를 갖는다
public class Explosion : MonoBehaviour
{
    Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Destroy(this.gameObject, anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
    }
}
