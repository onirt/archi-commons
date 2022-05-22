using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArChi.Controllers
{
    public interface IController<T>
    {
        T GetController();
    }
}
