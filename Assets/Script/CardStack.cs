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

        public void Add(PlayingCardGameObject result)
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
                result.transform.position = now.transform.position + new Vector3(0, -1.0f, -1.0f);
            }
        }
    }
}