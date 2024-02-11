using UnityEngine;
using TheKiwiCoder;
using System.Linq;
using System.Collections.Generic;
using System;
using Random = UnityEngine.Random;

public class AttemptAgressiveAction : ActionNode
{

    public RandomCallMethod callMethod;

    protected override void OnStart()
    {
        
    }

    protected override void OnStop()
    {
        
    }

    protected override State OnUpdate()
    {
        bool calledPerFrame;
        if (callMethod == RandomCallMethod.OnEachFrame) calledPerFrame = true;
        else calledPerFrame = false;

        float attackCheck = Random.Range(0.0f, 100.0f);

        if (context.difficultySettings.AggressionResult(attackCheck, calledPerFrame)) // If decided to perform an attack.
        {

            List<ActionAttack> hittingAttacks = context.hittingAttacks.Values.ToList(); // Get the list of all attacks that has a great possibility to make a hit on the opponent and get their name on a string list
            List<ActionAttack> allAttacks = context.selfFSM.AttackMoveDict.Values.ToList(); // Get the list of all attacks and get their name on a string list

            float attackAccuracyScore = Random.Range(0.0f, 100.0f); // Decide if the AI is going to perform an accurate or inaccurate attack.

            if (context.difficultySettings.AttackAccuracyResult(attackAccuracyScore))
            {
                if(hittingAttacks.Count > 0)
                {
                    blackboard.choosenAgressiveAction = hittingAttacks[Random.Range(0, hittingAttacks.Count)];
                    return State.Success;
                }
                else
                {
                    blackboard.choosenAgressiveAction = null;
                    return State.Failure;
                }
            }
            else {
                List<ActionAttack> nonHittingAttackNames = allAttacks.Except(hittingAttacks).ToList(); // Find only inaccurate attacks.
                blackboard.choosenAgressiveAction = nonHittingAttackNames[Random.Range(0, nonHittingAttackNames.Count)];
                return State.Success;
            }

        }
        else
        {
            // Debug.Log("Don't want to attack");
            blackboard.choosenAgressiveAction = null;
            return State.Failure;

        }
        
            
    }
}
