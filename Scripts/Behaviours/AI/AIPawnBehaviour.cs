using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    public class AIPawnBehaviour : AIBehaviour, IModel<PawnModel>
    {
        [SerializeField] protected PawnModel model;
        [SerializeField] protected Attributes attributes;

        public Attributes Attributes { get => attributes; set => attributes = value; }

        public virtual PawnModel GetModel()
        {
            return model;
        }

        public override void Init()
        {
            base.Init();
            attributes = new Attributes(GetModel().Attributes);
            Debug.Log($"[Attributes][Init][{name}] Attributes: {attributes.health}");
        }
    }
}
