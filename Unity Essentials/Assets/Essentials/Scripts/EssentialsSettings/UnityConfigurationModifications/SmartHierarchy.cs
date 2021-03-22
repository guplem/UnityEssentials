using System;
using UnityEngine.UIElements;
using UnityEditor.PackageManager.UI; // UnityEditor.PackageManager.UI is Meh...
using UnityEngine; 

#if UNITY_EDITOR
namespace Essentials.EssentialsSettings.UnityConfigurationModifications
{
    public class SmartHierarchy : VisualElement
    {

        string registryName = "", registryUrl = "";
        string[] scopes = {""};

        /*public void AddScopeUI()
        {
            UnityEditor.PackageManager.UI.UpmRegistryClient m_UpmRegistryClient;
            
            UnityEditor.PackageManager.UI.ServicesContainer instance = 
                ScriptableSingleton<UnityEditor.PackageManager.UI.ServicesContainer>.instance;
            
            m_UpmRegistryClient = instance.Resolve<UnityEditor.PackageManager.UI.UpmRegistryClient>();
            
            m_UpmRegistryClient.AddRegistry(registryName, registryUrl, scopes);
            
        }*/

/*        [SerializeField]
        private UpmAddRegistryOperation _addRegistryOperation;
        private UpmAddRegistryOperation addRegistryOperation => this._addRegistryOperation ?? (this._addRegistryOperation = new UpmAddRegistryOperation());

        public void AddScope()
        {
            this.addRegistryOperation.Add(registryName, registryUrl, scopes);
            UpmAddRegistryOperation registryOperation = this.addRegistryOperation;
            registryOperation.onProcessResult = registryOperation.onProcessResult + new Action<AddScopedRegistryRequest>(this.OnProcessAddRegistryResult);

            /*this.addRegistryOperation.onOperationError += (Action<IOperation, UIError>) ((op, error) =>
            {
                Action<string, UIError> registryOperationError = this.onRegistryOperationError;
                if (registryOperationError == null)
                    return;
                registryOperationError(name, error);
            });*/
/*       }
             private void OnProcessAddRegistryResult(AddScopedRegistryRequest request)
              {
                  if (!this.m_SettingsProxy.AddRegistry(request.Result))
                      return;
                  this.m_SettingsProxy.Save();
                  Action registriesModified = this.onRegistriesModified;
                  if (registriesModified != null)
                      registriesModified();
                  Client.Resolve();
              }
*/
          }
}
#endif