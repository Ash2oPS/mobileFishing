using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EtienneSibeaux.UI
{
    public class C_UI_NewFishNotification : CA_UIClass
    {
        [SerializeField] private Animator _animator;

        private Coroutine _curCor;

        public void NewFish()
        {
            if(_curCor != null ) 
                StopCoroutine( _curCor );

            _curCor = StartCoroutine(NotifDuration());
        }

        private IEnumerator NotifDuration()
        {
            _animator.SetTrigger("Appear");

            yield return new WaitForSeconds(3f);

            _animator.SetTrigger("Disappear");

            _curCor = null;
        }

    }
}
