﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Platformer camera script
/// Author: Indie Marc (Marc-Antoine Desbiens)
/// </summary>

namespace IndieMarc.Platformer
{

    public class FollowCamera : MonoBehaviour
    {
        [Header("Camera Target")]
        public GameObject target;
        public Vector3 target_offset;
        public float camera_speed = 5f;

        [Header("Level Limits")]
        public bool level_limits = true;
        public float level_bottom;
        public float level_left;
        public float level_right;
        
        private Camera cam;
        private Vector3 cur_pos;
        private GameObject lock_target = null;

        private Vector3 shake_vector = Vector3.zero;
        private float shake_timer = 0f;
        private float shake_intensity = 1f;

        private static FollowCamera _instance;

        void Awake()
        {
            _instance = this;
            cam = GetComponent<Camera>();
        }

        void LateUpdate()
        {
            GameObject cam_target = target;

            if (lock_target != null)
                cam_target = lock_target;

            if (cam_target != null)
            {
                //Find target
                Vector3 target_pos = cam_target.transform.position + target_offset;
                target_pos = LimitPos(target_pos);

                //Check if need to move
                Vector3 diff = target_pos - transform.position;
                if (diff.magnitude > 0.1f)
                {
                    //Move camera
                    transform.position = Vector3.SmoothDamp(transform.position, target_pos, ref cur_pos, 1f / camera_speed, Mathf.Infinity, Time.deltaTime);
                }
            }

            //Shake FX
            if (shake_timer > 0f)
            {
                shake_timer -= Time.deltaTime;
                shake_vector = new Vector3(Mathf.Cos(shake_timer * Mathf.PI * 8f) * 0.02f, Mathf.Sin(shake_timer * Mathf.PI * 7f) * 0.02f, 0f);
                transform.position += shake_vector * shake_intensity;
            }
        }

        private Vector3 LimitPos(Vector3 pos)
        {
            //Set level limits
            if (level_limits)
            {
                float fh = GetFrustrumHeight() / 2f;
                float fw = GetFrustrumWidth() / 2f;
                pos.x = Mathf.Max(level_left + fw, pos.x);
                pos.x = Mathf.Min(level_right - fw, pos.x);
                pos.y = Mathf.Max(level_bottom + fh, pos.y);
            }
            return pos;
        }

        public float GetFrustrumHeight()
        {
            if (cam.orthographic)
                return 2f * cam.orthographicSize;
            else
                return 2.0f * Mathf.Abs(transform.position.z) * Mathf.Tan(cam.fieldOfView * 0.5f * Mathf.Deg2Rad);
        }

        public float GetFrustrumWidth()
        {
            return GetFrustrumHeight() * cam.aspect;
        }

        public void LockCameraOn(GameObject ltarget)
        {
            lock_target = ltarget;
        }

        public void UnlockCamera()
        {
            lock_target = null;
        }

        public void MoveTo(Vector3 targ_pos)
        {
            targ_pos = LimitPos(targ_pos);
            transform.position = targ_pos + target_offset;
        }

        public void Shake(float intensity = 2f, float duration = 0.5f)
        {
            shake_intensity = intensity;
            shake_timer = duration;
        }

        public static FollowCamera Get()
        {
            return _instance;
        }

        public static Camera GetCamera()
        {
            if (_instance)
                return _instance.cam;
            return null;
        }
    }

}
