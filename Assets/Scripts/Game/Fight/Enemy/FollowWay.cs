using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameRole
{
    public class FollowWay:MonoBehaviour
    {
        public float _Speed;
        public UnityAction _Finish;
        private Queue<Vector3> _V3Way = new Queue<Vector3>();
        private bool _IsOpen;

        public void Move(List<Vector3> way)
        {
            for (int i = 0; i < way.Count; i++)
            {
                _V3Way.Enqueue(way[i]);
            }
            _IsOpen = true;
        }

        private void MoveFollowWay(Transform obj, Vector3 target, float speed)
        {
            Vector3 p = new Vector3(target.x, target.y, obj.position.z);
            obj.position = Vector3.MoveTowards(obj.position, p, speed * Time.deltaTime);
            if (Vector3.Distance(p, obj.position) < 0.1f)
            {
                //到达target
                _V3Way.Dequeue();
            }
        }

        private void Update()
        {
            if (!_IsOpen)
            {
                return;
            }
            if (_V3Way.Count > 0)
            {
                MoveFollowWay(transform, _V3Way.Peek(), _Speed);
            }
            else
            {
                _Finish?.Invoke();
            }
        }
    }
}
