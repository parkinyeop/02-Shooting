using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class HitEffect : MonoBehaviour
{
    Animator anim;
    float animLength;
    
    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        animLength = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, animLength);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
