using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    public class AIPawnBehaviour : AIBehaviour, IModel<PawnModel>
    {
        [SerializeField] protected PawnModel model;
        protected Attributes attributes;

        public Attributes Attributes { get => attributes; }

        public virtual PawnModel GetModel()
        {
            return model;
        }

        public override void Init()
        {
            base.Init();
            attributes = new Attributes(GetModel().Attributes);
        }
    }
}
