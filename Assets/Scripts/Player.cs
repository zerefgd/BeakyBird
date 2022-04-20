using UnityEngine;

public class Player : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 mousPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 postion = transform.position;
            postion.x = mousPos.x;
            transform.position = postion;
        }
    }
}
