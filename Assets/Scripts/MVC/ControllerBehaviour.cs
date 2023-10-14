using UnityEngine;

namespace MVC
{
    public abstract class ControllerBehaviour : MonoBehaviour, IController
    {
        #region IController<T>

        public object Model
        {
            get;
            private set;
        }

        public T GetModel<T>() where T : class
        {
            return Model as T;
        }

        #endregion

        protected abstract object CreateModel();

        protected virtual void Awake()
        {
            Model = CreateModel();
            IView view = GetComponent<IView>();
            view?.SetModel(Model);
        }
    }
}