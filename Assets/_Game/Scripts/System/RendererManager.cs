using UnityEngine.AddressableAssets;
using UnityEngine;

public class RendererManager : MonoBehaviour
{
    public static RendererManager Instance { get; private set; }
    [SerializeField] AssetReference[] model_assets;
    public AssetReference[] Model_Assets { get => model_assets; }

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
}
