using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder {

    // This is the blackboard container shared between all nodes.
    // Use this to store temporary data that multiple nodes need read and write access to.
    // Add other properties here that make sense for your specific use case.
    [System.Serializable]
    public class Blackboard {
        public Vector3 moveToPosition;
        public string choosenAgressiveAction;
        public string choosenDefensiveAction;
        public ActionAttack enemyAttackAction;

        public bool enemyAttackStarted;
        public bool EnemyAttackEnded;

        public bool selfAttackStarted;
        public bool selfAttackEnded;

        public void OnEnemyAttackStart()
        {
            Debug.Log("I've been Invoked!");
            enemyAttackStarted = true;
            EnemyAttackEnded = false;
        }

        public void OnEnemyAttackEnd()
        {
            enemyAttackStarted = false;
            EnemyAttackEnded = true;
        }

        public void OnSelfAttackStart()
        {
            Debug.Log("I've been Invoked!");
            selfAttackStarted = true;
            selfAttackEnded = false;
        }

        public void OnSelfAttackEnd()
        {
            selfAttackStarted = false;
            selfAttackEnded = true;
        }
    }
}