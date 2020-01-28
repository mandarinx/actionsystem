using System;
using System.Collections;
using UnityEngine;
using Altruist;
using JetBrains.Annotations;

namespace RL {

    [UsedImplicitly]
    [ActionSystem(typeof(MoveAction))]
    public class MoveSystem : IActionSystem {

        public Type TargetProperty => typeof(PropPosition);

        public IEnumerator Resolve(Item source, IAction sourceAction, Item target, Bridge bridge) {
            PropPosition propPos = Property.Get<PropPosition>(source);
            Coord startCoord = PropPosition.GetCoord(propPos);
            Vector3 start = source.transform.position;
            Vector3 end = target.transform.position;
            Vector3 dir = end - start;
            float dist = dir.magnitude;
            float travel = 0f;

            while (travel < dist) {
                Vector3 pos = start + dir * travel;
                source.transform.position = pos;
                PropPosition.SetWorldPos(propPos, pos);
                travel += Time.deltaTime * CFG.E_SPEED;
                yield return null;
            }
            
            source.transform.position = end;
            PropPosition.SetWorldPos(propPos, end);
            
            Debug.Log($"{source.name} moved from {startCoord.map} to {PropPosition.GetCoord(propPos).map}");
        }
    }
}
