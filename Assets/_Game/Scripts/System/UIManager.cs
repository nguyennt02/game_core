using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    [SerializeField] Transform modalParent;
    Stack<BaseUIRoot> modalUseds = new Stack<BaseUIRoot>();
    Dictionary<string, BaseUIRoot> modals = new Dictionary<string, BaseUIRoot>();
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        Init();
    }
    void Init()
    {
        foreach (var modal in RendererManager.Instance.Model_Assets)
        {
            Addressables.InstantiateAsync(modal, modalParent).Completed += (AsyncOperationHandle<GameObject> handle) =>
            {
                if (handle.Result.TryGetComponent(out BaseUIRoot uiRoot))
                {
                    modals.Add(uiRoot.name, uiRoot);
                    handle.Result.SetActive(false);
                }
                else
                {
                    handle.Release();
                }
            };
        }
    }
    public void ShowModal(string name)
    {
        ShowModal(modals[name]);
    }
    public void ShowModal(BaseUIRoot modal)
    {
        if (modalUseds.Count > 0)
        {
            if (modalUseds.Peek().name.Equals(modal.name))
            {
                Debug.LogWarning($"{modal.name} da duoc mo");
                return;
            }
            modalUseds.Peek().HideModal().onAfterHideModal = () => modal.ShowModal();
        }
        else
        {
            modal.ShowModal();
        }
        modalUseds.Push(modal);
    }

    public void HideModal()
    {
        if (modalUseds.Count > 0)
        {
            modalUseds.Pop().HideModal().onAfterHideModal = () =>
            {
                if (modalUseds.Count > 0)
                    modalUseds.Peek().ShowModal();
            };
        }
    }

    public void HideAllModal()
    {
        if (modalUseds.Count > 0)
        {
            modalUseds.Peek().HideModal();
            modalUseds.Clear();
        }
    }
}
