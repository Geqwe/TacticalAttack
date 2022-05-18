using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle;

public class PlayerInputManager : MonoBehaviour
{
    private BattleContoller _battleController;
    [SerializeField] private Texture2D cursorTextureGround;
    [SerializeField] private Texture2D cursorTextureAttack;
    private Camera _cam;
    [SerializeField] private GameObject _moveIndicator;
    private Vector2 _cursorOffset;

    void Start()
    {
        _battleController = FindObjectOfType<BattleContoller>();
        _cam = Camera.main;
        _cursorOffset = new Vector2(cursorTextureAttack.width/4, cursorTextureAttack.height/5);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(1)) {
            if(_battleController.PlayersTurn) {
                RaycastHit hit;
                Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit)) {
                    if(hit.transform.gameObject.layer == 7) { //enemy
                        _battleController.TroopAttack(hit.transform);
                    }
                    else if(hit.transform.gameObject.layer == 8) { //ground
                        _battleController.TroopMove(hit.point);
                        StopAllCoroutines();
                        StartCoroutine(ShowMoveIndicator(hit.point+new Vector3(0,1.5f,0)));
                    }
                }
            }
        }
        
        RaycastHit hitCursor;
        Ray rayCursor = _cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(rayCursor, out hitCursor)) {
            if(hitCursor.transform.gameObject.layer == 7) { //enemy
                Cursor.SetCursor(cursorTextureAttack, _cursorOffset, CursorMode.Auto);
            }
            else if(hitCursor.transform.gameObject.layer == 8) { //ground
                Cursor.SetCursor(cursorTextureGround, _cursorOffset, CursorMode.Auto);
            }
        }

        
    }

    private IEnumerator ShowMoveIndicator(Vector3 position) {
        _moveIndicator.transform.position = position;
        _moveIndicator.SetActive(true);
        yield return new WaitForSeconds(2f);
        _moveIndicator.SetActive(false);
    }
}
