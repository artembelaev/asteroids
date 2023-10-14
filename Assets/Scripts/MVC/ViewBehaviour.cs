using UnityEngine;

namespace MVC
{
    public abstract class ViewBehaviour: MonoBehaviour, IView
    {
        #region IView<T>

        public object Model
        {
            get;
            private set;
        }

        public void SetModel(object model)
        {
            Model = model;
        }

        #endregion
    }

}