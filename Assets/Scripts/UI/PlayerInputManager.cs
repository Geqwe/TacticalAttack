using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Battle;

public class PlayerInputManager : MonoBehaviour
{
    private BattleContoller _battleController;
    private Camera _cam;

    // Start is called before the first frame update
    void Start()
    {
        _battleController = FindObjectOfType<BattleContoller>();
        _cam = Camera.main;
    }

    // Update is called once per frame
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
                    }
                }
            }
        }
    }
}
