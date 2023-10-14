using UnityEngine;

namespace MVC
{
    public abstract class ControllerBehaviour<T> : MonoBehaviour, IController<T> where T : class
    {
        #region IController<T>

        public T Model
        {
            get;
            private set;
        }

        #endregion

        protected abstract T CreateModel();

        protected virtual void Awake()
        {
            Model = CreateModel();
            IView<T> view = GetComponent<IView<T>>();
            view?.SetModel(Model);
        }
    }
}