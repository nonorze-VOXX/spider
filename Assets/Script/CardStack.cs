using System;
using UnityEngine;

namespace Script
{
    public class CardStack : MonoBehaviour
    {
        private PlayingCardGameObject _head;

        private void Start()
        {
        }

        public void SetHead(PlayingCardGameObject head)
        {
            _head = head;
        }

        public PlayingCardGameObject GetHead()
        {
            return _head;
        }

        public void AddTailSetPosition(PlayingCardGameObject result)
        {
            if (_head == null)
            {
                _head = result;
                result.transform.position = transform.position;
            }
            else
            {
                var now = _head;
                while (now.GetNext() != null) now = now.GetNext();
                now.SetNext(result);
                result.transform.position = now.GetNextPosition();
            }
        }

        public void Connect(PlayingCardGameObject target)
        {
            var now = _head;
            while (now.GetNext() != null) now = now.GetNext();
            now.SetNext(target);

            while (now.GetNext() != null)
            {
                now = now.GetNext();
                now.SetHead(_head);
                now.SetStack(this);
            }
        }

        public void Disconnect(PlayingCardGameObject target)
        {
            var pre = FindPreNodeOfTarget(target);
            pre.SetNext(null);
            {
                target.SetHead(null);
                target.SetHead(null);
                var now = target;
                while (now.GetNext() != null)
                {
                    now = now.GetNext();
                    now.SetHead(target);
                }
            }
        }

        public PlayingCardGameObject GetTail()
        {
            return FindPreNodeOfTarget(null);
        }

        public PlayingCardGameObject FindPreNodeOfTarget(PlayingCardGameObject target)
        {
            var now = _head;
            while (now.GetNext() != target)
            {
                if (now.GetNext() == null) throw new Exception("target no found;");
                now = now.GetNext();
            }

            return now;
        }
    }
}