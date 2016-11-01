using UnityEngine;

public class Raycast_Mgr : MonoBehaviour {
    private RaycastHit hit;
    private GameObject target;
    private float rotSpeed = 5.0f;    
	
	void Update ()
    {
        int cnt = Input.touchCount; //현재 터치 갯수 반환

        for (int i = 0; i < cnt; ++i)
        {
            Touch touch = Input.GetTouch(i);
            Vector2 pos = touch.position;

            if (touch.phase == TouchPhase.Began)
            {                
                Ray ray = Camera.main.ScreenPointToRay(pos); //레이를 카메라로부터 Pos로 발사
                if (Physics.Raycast(ray, out hit, Mathf.Infinity, 1 << 8))
                {
                    if (hit.collider.tag == "Dragon" || hit.collider.tag == "Witch") //케릭터 오브젝트가 히트 되었을 경우만 감지
                    {
                        target = hit.collider.gameObject;                       
                    }
                }
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                if (target != null) //움직일 오브젝트 가 있을경우 해당 오브젝트 회전
                {
                    Vector2 prePos = touch.deltaPosition;
                    Transform tr = target.GetComponentsInParent<Transform>()[2];
                    tr.Rotate(0, -prePos.x, 0);
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                target = null;
            }
        }
    }
}
