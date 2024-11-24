using UnityEngine;

public class BtnTest : MonoBehaviour
{
    int i = 0;
    public void Test()
    {
        if (i > 4) i = 0;
        switch (i)
        {
            case 0:
                UIManager.Instance.ShowModal("Canvas_1(Clone)");
                break;
            case 1:
                UIManager.Instance.ShowModal("Canvas_2(Clone)");
                break;
            case 2:
                UIManager.Instance.ShowModal("Canvas_3(Clone)");
                break;
            case 3:
                UIManager.Instance.ShowModal("Canvas_4(Clone)");
                break;
            case 4:
                UIManager.Instance.ShowModal("Canvas_5(Clone)");
                break;
        }
        i++;
    }

    public void Hide()
    {
        UIManager.Instance.HideModal();
    }

    public void HideAll()
    {
        UIManager.Instance.HideAllModal();

    }
}
