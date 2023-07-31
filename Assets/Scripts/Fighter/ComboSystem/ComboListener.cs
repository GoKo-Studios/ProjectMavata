using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboListener
{
    private FighterStateMachine _ctx;

    private Dictionary<string, ComboMoveSpecs> _comboMovesDict;
    private Dictionary<string, ComboMoveSpecs> _currentSearchDict;

    //private int _comboFrameLimit = 1500;
    //private int _currentMoveNumber;
    // private string _currentAttackName;
    // private string _previousAttackName;
    private int _currentFrame;
    private ActionAttack _receivedAttack;
    private bool _doesDictContainMove;
    public bool isActive;

    private ActionAttack _attackAction = null;

    public ComboListener(FighterStateMachine ctx){
        _ctx = ctx;
        _comboMovesDict = new Dictionary<string, ComboMoveSpecs>();
        ResetListener();
        ArrangeMovesList();
        _receivedAttack = null;
    }

    public void FixedUpdate(){
        int receivedAttackFrames = 0;
        if (_receivedAttack is not null) receivedAttackFrames = _receivedAttack.FrameLenght; 

        if(_currentFrame > receivedAttackFrames + _ctx.ComboBuffer){ // Break the combo listener after the attack ends and a window definedand (ComboBuffer) is passed.
            ResetListener();
        }

        _currentFrame++;
    }

    public ActionAttack AttackOverride(ActionAttack action){
        _receivedAttack = action;
        _attackAction = null;
        //Debug.Log("Current Mov Number: " + _currentMoveNumber);
        //Debug.Log("Current ActionAttack : " + newAttack.name);

        if(_currentSearchDict == null) _currentSearchDict = _comboMovesDict;


        if(!_currentSearchDict.ContainsKey(action.name)){
            ResetListener();
            // Altough the player might've broken the combo the move performed might be the starter of another combo.
            if(_comboMovesDict.ContainsKey(action.name)){ 
                _currentSearchDict = _comboMovesDict[action.name].possibleNextMoves;
            }
            return action;
        }
        else{
            _currentFrame = 0;
            if(_currentSearchDict[action.name].willOverride){
                _attackAction = _currentSearchDict[action.name].moveToOverride;
            }
            else{
                _attackAction = action;
            }
            _currentSearchDict = _currentSearchDict[action.name].possibleNextMoves;
        }

        //_currentMoveNumber++;
        return _attackAction;
    }

    private void ResetListener(){
        _currentFrame = 0;
        //_currentMoveNumber = 1;
        _currentSearchDict = null;
        //_currentAttackName = null;
        _doesDictContainMove = false;
        isActive = false;
        _receivedAttack = null;
    }


    private void ArrangeMovesList(){
        Dictionary<string, ComboMoveSpecs> tempDictWriter;
        for(int i = 0; i < _ctx.CombosArray.Length; i++){
            tempDictWriter = this._comboMovesDict;
            for(int j = 0; j < _ctx.CombosArray[i].moves.Length; j++){
                ComboMoveSpecs comboMoveSpecs = _ctx.CombosArray[i].moves[j];
                string comboAttackName = comboMoveSpecs.theMove.name;
                //Debug.Log("It's me!" + comboAttackName);
                if(!tempDictWriter.ContainsKey(comboAttackName)){
                    tempDictWriter.Add(comboAttackName, comboMoveSpecs);
                }
                //comboMoveSpecs.DeclareNewPossibleNextMoves();
                tempDictWriter = tempDictWriter[comboAttackName].possibleNextMoves;  
            }
        }
    }

    public Dictionary<string, ComboMoveSpecs> GetCurrentSearchDict()
    {
        return _currentSearchDict;
    }
}
//ş
