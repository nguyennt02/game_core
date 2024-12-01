using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
public static class SaveToRemoteSystem
{
    public static async Task WriteAlTo(string json, string serverUrl)
    {
        // Tạo request
        using (UnityWebRequest request = new UnityWebRequest(serverUrl, "POST"))
        {
            // Chuyển chuỗi JSON thành byte[] và thêm vào body của request
            byte[] jsonToSend = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();

            // Set header để server nhận biết đây là dữ liệu JSON
            request.SetRequestHeader("Content-Type", "application/json");

            // Gửi request
            var operation = request.SendWebRequest();

            // Chờ hoàn tất request (dùng Task để chờ không đồng bộ)
            while (!operation.isDone)
                await Task.Yield();

            // Kiểm tra kết quả
            if (request.result == UnityWebRequest.Result.Success)
                Debug.Log("Data successfully uploaded! Response: " + request.downloadHandler.text);
            else
                Debug.LogError("Error uploading data: " + request.error);
        }
    }

    // Phương thức tải JSON từ server
    public static async Task<string> LoadDataFrom(string serverUrl)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(serverUrl))
        {
            var operation = request.SendWebRequest();

            // Chờ request hoàn tất
            while (!operation.isDone)
                await Task.Yield();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonData = request.downloadHandler.text;
                Debug.Log("JSON downloaded successfully: " + jsonData);
                return jsonData;
            }
            else
            {
                Debug.LogError("Error downloading JSON: " + request.error);
                return null;
            }
        }
    }
}
