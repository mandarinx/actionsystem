using System;
using System.Collections;
using AptGames;
using UnityEngine;
using JetBrains.Annotations;

namespace RL {

    [UsedImplicitly]
    [ActionSystem(typeof(MoveAction))]
    public class MoveSystem : IActionSystem {

        public Type TargetProperty => typeof(PropPosition);

        public void Resolve(Item source, IAction action, Item target) {
            Vector3 targetPos = target.transform.position;
            Coroutines.Run(Move(targetPos - source.transform.position,
                                targetPos,
                                source,
                                Property.Get<PropPosition>(source)));
        }

        private static IEnumerator Move(Vector3      direction,
                                        Vector3      endPos,
                                        Item         source,
                                        PropPosition propPos) {

            Coord startCoord = PropPosition.GetCoord(propPos);
            Vector3 start = source.transform.position;
            float dist = direction.magnitude;
            float travel = 0f;

            while (travel < dist) {
                Vector3 pos = start + direction * travel;
                source.transform.position = pos;
                PropPosition.SetWorldPos(propPos, pos);
                travel += Time.deltaTime * CFG.E_SPEED;
                yield return null;
            }

            source.transform.position = endPos;
            PropPosition.SetWorldPos(propPos, endPos);
            
            Debug.Log($"{source.name} moved from {startCoord.map} to {PropPosition.GetCoord(propPos).map}");
        }
    }
}
