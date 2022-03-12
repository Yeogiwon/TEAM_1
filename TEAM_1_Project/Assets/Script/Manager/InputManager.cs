using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class InputManager : MonoBehaviour
{
    public enum E_CLICKERSTATE { CREATEUNIT1, CREATEUNIT2, CREATEUNIT3, CREATEUNIT4, MOVE, STANDBY }
    public E_CLICKERSTATE e_CLICKERSTATE {get;set;}
    public Unit _currSelectedUnit = null;
    public void SetClickerState(int idx)
    {
        switch (idx)
        {
            case 0:
                e_CLICKERSTATE = E_CLICKERSTATE.CREATEUNIT1;
                break;
            case 1:
                e_CLICKERSTATE = E_CLICKERSTATE.CREATEUNIT2;
                break;
            case 2:
                e_CLICKERSTATE = E_CLICKERSTATE.CREATEUNIT3;
                break;
            case 3:
                e_CLICKERSTATE = E_CLICKERSTATE.CREATEUNIT4;
                break;
            case 4:
                e_CLICKERSTATE = E_CLICKERSTATE.MOVE;
                break;
            case 5:
                e_CLICKERSTATE = E_CLICKERSTATE.STANDBY;
                break;
            default:
                break;
        }
    }

    public E_CLICKERSTATE Get_ClickerState()
    {
        return e_CLICKERSTATE;
    }
    void Start()
    {
        
    }

    void Update()
    {
        LeftClick();
        RightClick();
    }

    public void LeftClick()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        if (EventSystem.current.IsPointerOverGameObject()) return; // UI 클릭하면 종료
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero); // 카메라에서 화면으로 광선을 쏴서 맞는 물체 판별
        if (GameManager.GetInstance().uiManager._popupUIs.Count > 0) Destroy(GameManager.GetInstance().uiManager._popupUIs.Pop()); // 팝업 UI (현재는 유닛 정보) 가 떠있으면 삭제
        if (hit.collider == null)
        {
            return;
        }
        Debug.Log($"{hit.collider.name} 클릭");

        var _SceneManager = GameManager.GetInstance().sceneManager;

		if(hit.collider.gameObject.GetComponent<PlaceObject>() != null) {
            _SceneManager.onClickPlaceObject(hit.collider.gameObject);    
        }
    }
    public void RightClick()
    {
        if (!Input.GetMouseButtonDown(1)) return;
        if (EventSystem.current.IsPointerOverGameObject()) return;
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (GameManager.GetInstance().uiManager._popupUIs.Count > 0) Destroy(GameManager.GetInstance().uiManager._popupUIs.Pop());
        if (hit.collider == null)
        {
            return;
        }
        if(hit.collider.gameObject.GetComponent<Unit>() != null) // 광선을 맞은 물체가 Unit 컴포넌트를 가지고 있으면
        {
            GameObject _uiInfo = Instantiate(GameManager.GetInstance().resourceManager.LoadUI("UI_Unit_Info"));
            _uiInfo.transform.SetParent(GameManager.GetInstance().sceneManager.UI_Parent.transform);
            GameManager.GetInstance().uiManager._popupUIs.Push(_uiInfo);
            Image _iamge = _uiInfo.GetComponentInChildren<Image>();
            _iamge.gameObject.transform.position = Input.mousePosition + Vector3.right * GameManager.GetInstance().uiManager._uiInfoOffset;
            _iamge.sprite = hit.collider.gameObject.GetComponent<Unit>().character.sprite;
            _iamge.color = hit.collider.gameObject.GetComponent<Unit>().character.color;
        }
    }
}
