using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//[ExecuteAlways]
internal class AnimatedTransform : ProjectBehaviour
{

    private List<Animation> _animations = new List<Animation>();

    private class Animation
    {
        public float _duration = 1f;
        public float _returnDuration = 1f;
        public bool _return = false;
        public float _timeRemaining;
        //public Vector3 _fromEulerAngles;
        //public Vector3 _targetEulerAngles;
        internal Vector3 _totalOffset;
        internal Vector3 _offsetPerSecond;
    }

    //public Easing Easing = Easing.Linear;

    // Update is called once per frame
    protected virtual void Update()
    {
        if (_animations.Count == 0)
        {
            return;
        }

        UpdateAnimation(_animations[0]);
    }

    void UpdateAnimation(Animation animation)
    {
        //Debug.Log("UpdateAnimation");

        if (animation._timeRemaining <= 0f)
        {
            animation._timeRemaining = 0f;

            if (!animation._return)
            {
                _animations.Remove(animation);

                //animation._fromEulerAngles = animation._targetEulerAngles;
            }
            else
            {
                animation._return = false;
                animation._timeRemaining = animation._duration = animation._returnDuration;
                //animation._targetEulerAngles = animation._fromEulerAngles;
                //animation._fromEulerAngles = transform.localEulerAngles;
                animation._totalOffset = new Vector3(-animation._totalOffset.x, -animation._totalOffset.y, -animation._totalOffset.z);
                animation._offsetPerSecond = (1f / animation._duration) * animation._totalOffset;
            }
        }

        animation._timeRemaining -= Time.deltaTime;

        //var t = Ease((animation._duration - animation._timeRemaining) / animation._duration, Easing);

        //Debug.Log($"t: {t}");

        //var r = LerpAngle(animation._fromEulerAngles, animation._targetEulerAngles, t);

        //var r = transform.localEulerAngles + (animation._targetOffset * t);

        //var r = transform.localEulerAngles + (animation._offsetPerSecond * Time.deltaTime);

        transform.localEulerAngles += (animation._offsetPerSecond * Time.deltaTime);

        //Debug.Log($"transform.localEulerAngles: {transform.localEulerAngles.x}, {transform.localEulerAngles.y}, {transform.localEulerAngles.z}");
    }

    //private Vector3 LerpAngle(Vector3 from, Vector3 target, float t)
    //{
    //    return new Vector3(
    //        Mathf.LerpAngle(from.x, target.x, t),
    //        Mathf.LerpAngle(from.y, target.y, t),
    //        Mathf.LerpAngle(from.z, target.z, t));
    //}

    //public AnimatedTransform RotateTowards(float x, float y, float z)
    //{
    //    return RotateTowards(x, y, z, 1f);
    //}

    //public AnimatedTransform RotateTowards(float x, float y, float z, float duration)
    //{
    //    var animation = new Animation
    //    {
    //        _fromEulerAngles = transform.localEulerAngles,
    //        _timeRemaining = duration,
    //        _duration = duration,
    //        _targetEulerAngles = new Vector3(x, y, z),
    //    };

    //    _animations.Add(animation);

    //    return this;
    //}

    //public AnimatedTransform RotateTowardsReturn(float x, float y, float z, float duration)
    //{
    //    return RotateTowardsReturn(x, y, z, duration, duration);
    //}

    //public AnimatedTransform RotateTowardsReturn(float x, float y, float z, float duration, float returnDuration)
    //{
    //    var animation = new Animation
    //    {
    //        _return = true,
    //        _fromEulerAngles = transform.localEulerAngles,
    //        _timeRemaining = duration,
    //        _duration = duration,
    //        _targetEulerAngles = new Vector3(x, y, z),
    //    };

    //    _animations.Add(animation);

    //    return this;
    //}

    //public AnimatedTransform RotateAdd(float x, float y, float z)
    //{
    //    return RotateAdd(x, y, z, 1f);
    //}

    //public AnimatedTransform RotateAdd(float x, float y, float z, float duration)
    //{
    //    var animation = new Animation
    //    {
    //        _fromEulerAngles = transform.localEulerAngles,
    //        _timeRemaining = duration,
    //        _duration = duration,
    //        _targetEulerAngles = new Vector3(transform.localEulerAngles.x + x, transform.localEulerAngles.y + y, transform.localEulerAngles.z + z),
    //    };

    //    _animations.Add(animation);

    //    return this;
    //}

    //public AnimatedTransform RotateAddReturn(float x, float y, float z, float duration)
    //{
    //    return RotateAddReturn(x, y, z, duration, duration);
    //}

    public AnimatedTransform RotateAddReturn(float x, float y, float z, float duration, float returnDuration)
    {
        var animation = new Animation
        {
            _return = true,
            _returnDuration = returnDuration,
            //_fromEulerAngles = transform.localEulerAngles,
            _timeRemaining = duration,
            _duration = duration,
            //_targetEulerAngles = new Vector3(transform.localEulerAngles.x + x, transform.localEulerAngles.y + y, transform.localEulerAngles.z + z),
            _totalOffset = new Vector3(x, y, z),
            _offsetPerSecond = (1f / duration) * new Vector3(x, y, z),
        };

        _animations.Add(animation);

        return this;
    }

    private float Ease(float t, Easing easing)
    {
        switch (easing)
        {
            case Easing.InQuadratic:
                return t * t;
            case Easing.OutQuadratic:
                return 1 - Ease(1 - t, Easing.InQuadratic);
            case Easing.Quadratic:
                if (t < 0.5)
                {
                    return Ease(t * 2.0f, Easing.InQuadratic) / 2.0f;
                }
                return 1 - Ease((1 - t) * 2.0f, Easing.InQuadratic) / 2.0f;
            default:
                return t;
        }
    }
}
