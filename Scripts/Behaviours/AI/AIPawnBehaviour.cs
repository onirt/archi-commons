using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    public class AIPawnBehaviour : AIBehaviour, IModel
    {
        protected Attributes attributes;

        public Attributes Attributes { get => attributes; }

        public virtual Attributes GetModelAttributes()
        {
            throw new System.NotImplementedException();
        }

        public override void Init()
        {
            base.Init();
            attributes = new Attributes(GetModelAttributes());
        }
    }
}
