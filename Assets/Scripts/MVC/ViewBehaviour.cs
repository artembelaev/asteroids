using UnityEngine;

namespace MVC
{
    public abstract class ViewBehaviour<T> : MonoBehaviour, IView<T> where T : class
    {
        #region IView<T>

        public T Model
        {
            get;
            private set;
        }

        public void SetModel(T model)
        {
            Model = model;
        }

        #endregion
    }

}