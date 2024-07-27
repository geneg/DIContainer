
using System;
using System.Collections.Generic;

namespace com.fourcatsgames.DI
{
    public class DIContainer
    {
        private readonly DIContainer _parent;
        private readonly Dictionary<Type, DIRegistration> _registrations = new Dictionary<Type, DIRegistration>();
        private readonly HashSet<Type> _resolutions = new HashSet<Type>();
        
        public DIContainer(DIContainer parent = null)
        {
            _parent = parent;
        }

        public void RegisterSingle<T>(DIRegistration.RegistrationFactory factory)
        {
            Register(typeof(T), factory, true);
        }
        
        public void RegisterWithNew<T>(DIRegistration.RegistrationFactory factory)
        {
            Register(typeof(T), factory, false);
        }

        public void RegisterInstance<T>(T instance)
        {
            Register(typeof(T), instance);
        }

        public T Resolve<T>()
        {
            if (_resolutions.Contains(typeof(T)))
            {
                throw new Exception($"Cyclic dependency for type: {typeof(T)}");
            }

            _resolutions.Add(typeof(T));

            try
            {
                if (_registrations.TryGetValue(typeof(T),out DIRegistration registration))
                {
                    if (registration.IsSingle)
                    {
                        if (registration.Instance == null && registration.Factory != null)
                        {
                            registration.Instance = registration.Factory(this);    
                        }
                   
                        return (T) registration.Instance;
                    }

                    return (T)registration.Factory(this);
                }

                if (_parent != null)
                {
                    return _parent.Resolve<T>();
                }
            }
            finally
            {
                _resolutions.Remove(typeof(T));
            }
            
            
            throw new Exception($"Can not resolve, no such type: {typeof(T)}");
        }
        
        private void Register(Type key, DIRegistration.RegistrationFactory factory, bool isSingle)
        {
            if (_registrations.ContainsKey(key))
            {
                throw new Exception($"Factory registration with key: {key} already exists.");
            }

            _registrations[key] = new DIRegistration
            {
                Factory = factory,
                IsSingle = isSingle,
            };
        }
        
        private void Register<T>(Type key, T instance)
        {
            if (_registrations.ContainsKey(key))
            {
                throw new Exception($"Instance registration with key: {key} already exists.");
            }

            _registrations[key] = new DIRegistration
            {
                Instance = instance,
                IsSingle = true,
            };
        }
    }
}
