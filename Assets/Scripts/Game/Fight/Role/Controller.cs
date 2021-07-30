using ChunkGame.Lua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GameRole
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class Controller : MonoBehaviour
    {

        private bool IsSelect = false;

        private void Start() 
        { 

        }

        private void FixedUpdate()
        {
            if (IsSelect)
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(worldPos.x,worldPos.y,0);
            }
        }

        private void OnMouseDown()
        {
            IsSelect = true;
        }

        private void OnMouseUp()
        {
            IsSelect = false;
        }
    }
}