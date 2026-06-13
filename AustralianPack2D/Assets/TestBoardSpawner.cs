using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBoardSpawner : MonoBehaviour
{
    [Header("Board")]
    public RectTransform board;

    [Header("Prefab")]
    public RectTransform cardPrefab;

    [Header("Configs")]
    public List<BoardLevelConfig> configs = new();

    [Header("Layout")]
    public float padding = 40f;
    public float spacing = 8f;

    [Range(0f, 1f)]
    public float fillFactor = 0.85f;

    public float minCellSize = 70f;
    public float maxCellSize = 180f;

    [SerializeField] private BoardLevelType levelTypeCurrent;

    private readonly List<RectTransform> spawned = new();

    private BoardLevelConfig GetConfig(BoardLevelType type)
    {
        return configs.Find(c => c.type == type);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Spawn(levelTypeCurrent);
        }
    }

    public void Spawn(BoardLevelType type)
    {
        Clear();

        var config = configs.Find(c => c.type == type);

        if (config == null)
        {
            Debug.LogError("Config not found");
            return;
        }

        fillFactor = config.fillFactor;

        SpawnAuto(config.totalCards);
    }

    private void SpawnAuto(int totalCards)
    {
        float width = board.rect.width;
        float height = board.rect.height;

        float usableWidth = width - padding * 2f;
        float usableHeight = height - padding * 2f;

        // ─────────────────────────────
        // 1. find best columns
        // ─────────────────────────────
        int bestColumns = 1;
        float bestCellSize = 0f;
        int bestRows = 0;

        for (int c = 1; c <= totalCards; c++)
        {
            int rows = Mathf.CeilToInt(totalCards / (float)c);

            float cellW = (usableWidth - (c - 1) * spacing) / c;
            float cellH = (usableHeight - (rows - 1) * spacing) / rows;

            float cellSize = Mathf.Min(cellW, cellH);

            if (cellSize > bestCellSize)
            {
                bestCellSize = cellSize;
                bestColumns = c;
                bestRows = rows;
            }
        }

        int columns = bestColumns;
        int rowsFinal = bestRows;

        float cell = Mathf.Clamp(bestCellSize, minCellSize, maxCellSize);

        // ─────────────────────────────
        // 2. recompute spacing
        // ─────────────────────────────
        float usedW = cell * columns;
        float usedH = cell * rowsFinal;

        float spacingX = columns > 1
            ? (usableWidth - usedW) / (columns - 1)
            : 0;

        float spacingY = rowsFinal > 1
            ? (usableHeight - usedH) / (rowsFinal - 1)
            : 0;

        spacingX = Mathf.Lerp(spacing, spacingX, fillFactor);
        spacingY = Mathf.Lerp(spacing, spacingY, fillFactor);

        // ─────────────────────────────
        // 3. final grid size
        // ─────────────────────────────
        float finalW = cell * columns + spacingX * (columns - 1);
        float finalH = cell * rowsFinal + spacingY * (rowsFinal - 1);

        Vector2 start = new Vector2(
            -finalW / 2f + cell / 2f,
             finalH / 2f - cell / 2f
        );

        // ─────────────────────────────
        // 4. spawn
        // ─────────────────────────────
        for (int i = 0; i < totalCards; i++)
        {
            int x = i % columns;
            int y = i / columns;

            RectTransform card = Instantiate(cardPrefab, board);
            spawned.Add(card);

            card.sizeDelta = new Vector2(cell, cell);

            card.anchoredPosition = new Vector2(
                start.x + x * (cell + spacingX),
                start.y - y * (cell + spacingY)
            );
        }
    }

    private void Clear()
    {
        foreach (var c in spawned)
            if (c != null)
                Destroy(c.gameObject);

        spawned.Clear();
    }
}

[System.Serializable]
public class BoardLevelConfig
{
    public BoardLevelType type;

    [Header("Cards")]
    public int totalCards;

    [Header("Layout")]
    public int columns;

    [Range(0f, 1f)]
    public float fillFactor = 0.85f;
}

public enum BoardLevelType
{
    Level4,
    Level8,
    Level16,
    Level32,
    Level64
}
