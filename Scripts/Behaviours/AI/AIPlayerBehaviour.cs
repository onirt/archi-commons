using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    public class AIPlayerBehaviour : AIPawnBehaviour, ITakeDamage
    {
        //[SerializeField] protected PlayerModel model;
        public void Take(Attributes attributes)
        {
            throw new System.NotImplementedException();
        }
        public override Attributes GetModelAttributes()
        {
            return base.GetModelAttributes();
        }
    }
}
