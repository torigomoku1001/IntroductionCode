using UnityEngine;
using System.Collections.Generic;

public class GameObjectScrollController : MonoBehaviour, IInfiniteScrollSetup
{
    [SerializeField]
    private GameObject[] gameObjectPrefabs; // 9個のゲームオブジェクトのプレハブ

    private Dictionary<int, GameObject> activeObjects = new Dictionary<int, GameObject>();

    public void OnUpdateItem(int index, GameObject item)
    {
        // プレハブのインデックスを計算（9個のオブジェクトをループ）
        int prefabIndex = index % gameObjectPrefabs.Length;
        
        // 既存のオブジェクトを破棄
        if (activeObjects.ContainsKey(index))
        {
            Destroy(activeObjects[index]);
        }

        // 新しいオブジェクトを生成
        GameObject newObject = Instantiate(gameObjectPrefabs[prefabIndex], item.transform);
        activeObjects[index] = newObject;
    }

    public void OnPostSetupItems()
    {
        // 初期設定後の処理（必要に応じて）
    }

    private void OnDestroy()
    {
        // クリーンアップ
        foreach (var obj in activeObjects.Values)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
        activeObjects.Clear();
    }
}