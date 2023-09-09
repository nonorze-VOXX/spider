using UnityEngine;

namespace Script
{
    public class CardStack : MonoBehaviour
    {
        private PlayingCardGameObject head;

        private void Start()
        {
        }

        public PlayingCardGameObject GetHead()
        {
            return head;
        }

        public void Add(PlayingCardGameObject result)
        {
            if (head == null)
            {
                head = result;
                result.transform.position = transform.position;
            }
            else
            {
                var now = head;
                while (now.GetNext() != null) now = now.GetNext();
                now.SetNext(result);
                result.transform.position = now.transform.position + new Vector3(0, -1.0f, -1.0f);
            }
        }
    }
}