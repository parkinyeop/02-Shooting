using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public Transform[] bgSlots;
    public float scrollSpeed = 2.5f;

    const float Background_Width = 13.6f;

    protected virtual void Awake()
    {
        bgSlots = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) // for반복은 정확한 인덱스가 필요할때 유리
        {
            bgSlots[i] = transform.GetChild(i);
        }
    }

    private void Update()
    {
        float minusX = transform.position.x - Background_Width;

        for (int i = 0; i < bgSlots.Length; i++)
        {
            bgSlots[i].Translate(scrollSpeed * Time.deltaTime * -transform.right);

            if (bgSlots[i].position.x < minusX)
            {
                BgMove(i);
            }
        }

        //foreach (Transform slot in bgSlots) //for 보다 빠름
        //{
        //    slot.Translate(scrollSpeed * Time.deltaTime * -transform.right);

        //    if (slot.position.x < minusX)
        //    {
        //        //Debug.Log($"{slot.name}은 충분히 왼쪽으로 이동했다.");

        //        //오른쪽으로 Background_Width의 3배(bgSlots.Length에 3개가 들어있다)만큼 이동
        //        slot.Translate(Background_Width * bgSlots.Length * transform.right);
        //    }
        //}
    }

    public virtual void BgMove(int index)
    {
        bgSlots[index].Translate(Background_Width * bgSlots.Length * transform.right);
    }
}
