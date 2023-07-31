using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class AIDecisionMechanism
{
    private AIBrain _brain;
    private AIDifficultySettings _aiDifficulty;

    private FighterStateMachine _selfCtx;
    private FighterStateMachine _enemyCtx;

    private List<string> fighterAttacks;

    private float _optimalDistance;


    public AIDecisionMechanism(AIBrain brain, AIDifficultySettings difficulty, FighterStateMachine selfStateMachine, FighterStateMachine enemyStateMachine)
    {
        _brain = brain;
        _aiDifficulty = difficulty;
        _selfCtx = selfStateMachine;
        _enemyCtx = enemyStateMachine;
    }

    // AttemptAggresiveAciton needs expection handling (hittingAttacks list may be empty)!
    public string AttemptAggresiveAciton(string callMethod)
    {
        bool callMethodArguement;

        if (callMethod == "Update") callMethodArguement = true;
        else callMethodArguement = false;

        float attackScore = Random.Range(0.0f, 100.0f);

        if (_aiDifficulty.AggressionResult(attackScore, callMethodArguement)) // If decided to perform an attack.
        {
            if (!_selfCtx.ComboListener.isActive) // If not inside a combo phase
            {
                float attackAccuracyScore = Random.Range(0.0f, 100.0f); // Decide if the AI is going to perform an accurate or inaccurate attack.

                List<string> hittingAttackNames = _brain.HittingAttacks.Keys.ToList(); // Get the list of all attacks that has a great possibility to make a hit on the opponent and get their name on a string list
                List<string> attackNames = _enemyCtx.AttackMoveDict.Keys.ToList(); // Get the list of all attacks and get their name on a string list

                if (_aiDifficulty.AttackAccuracy(attackAccuracyScore)){
                    List<string> nonHittingAttackNames = attackNames.Except(hittingAttackNames).ToList(); // Find only inaccurate attacks.
                    return nonHittingAttackNames[Random.Range(0, nonHittingAttackNames.Count())];
                }
                else{                    
                    return hittingAttackNames[Random.Range(0, hittingAttackNames.Count())];
                }               

            }
            else
            {
                float comboProbability = Random.Range(0.0f, 100.0f);

                List<string> comboMovesList = _selfCtx.ComboListener.GetCurrentSearchDict().Keys.ToList();
                List<string> hittingAttackNames = _brain.HittingAttacks.Keys.ToList();

                List<string> hittingComboAttacks = hittingAttackNames.Intersect(comboMovesList).ToList();

                if (_aiDifficulty.ComboResult(comboProbability)) // If proficient enough to know which move to do to keep the combo active.
                {

                    return hittingComboAttacks[Random.Range(0, hittingComboAttacks.Count())];
                }
                else
                {
                    List<string> attackMovesList = _selfCtx.AttackMoveDict.Keys.ToList();

                    attackMovesList = attackMovesList.Except(hittingComboAttacks).ToList();

                    return attackMovesList[Random.Range(0, attackMovesList.Count())];
                }
                
            }
            
        }
        else
        {

            return "Fail";

        }
            
    }

    public string TakeDefensiveAction(ActionAttack enemyAttack)
    {
        float defenseScore = Random.Range(0.0f, 100.0f);
        bool successfulDefensiveAction = _aiDifficulty.DefensiveActionResult(defenseScore);

        if (successfulDefensiveAction) // AI Descided to take a Defensive Action (Options are; Dodge or Counter).
        {
            string actionType;
            Debug.Log("There are total of " + _brain.HittingAttacks.Count() + " attacks that will hit.");
            if (_brain.HittingAttacks.Count() == 0) // If AI doesn't have any attack that will hit the opponent then it has to dodge
            {
                actionType = "Dodge";
            }
            else { // If AI has an attack or attacks that will hit then it should decide.
                float actionTypeScore = Random.Range(0.0f, 100f);

                actionType = _aiDifficulty.DefensiveActionType(actionTypeScore);
            }

            Debug.Log("AI Decided to: " + actionType);

            if (actionType == "Dodge") // AI Decided To Dodge.
            {
                return actionType;
            }
            else // AI Decided to counter attack
            {
                // If the enemy is attacking(check this just to be safe) also this way we can access the attack action that the enemy is currently performing.
                if (_enemyCtx.CurrentState.GetCurrentSubState is FighterAttackState) // 
                {
                    List<string> viableAttackOptions = new List<string>();

                    foreach (KeyValuePair<string, ActionAttack> attack in _brain.HittingAttacks) // Get attacks that can counter the opponent.
                    {
                        if(attack.Value.StartFrames < enemyAttack.StartFrames - 2)
                        {
                            viableAttackOptions.Add(attack.Key);
                        }
                    }
                    return viableAttackOptions[Random.Range(0, viableAttackOptions.Count())];
                    
                }
                else
                {
                    return "Fail";
                }
            }
        }
        else
        {
            return "Fail";
        }
    }

}
