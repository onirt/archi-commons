using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi
{
    public interface IModel<T>
    {
        T GetModel();
    }
}
