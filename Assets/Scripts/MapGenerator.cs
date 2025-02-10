using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    public int height = 6;
    public int width = 12;
    private readonly List<Path> _paths = new(); // 생성된 경로 리스트
    private readonly List<RoomNode> _rooms = new(); // 생성된 방 리스트

    private void Start()
    {
        GenerateMap();
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < height; i++)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(new Vector3(0, i, 0), 0.2f);

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(new Vector3(width - 1, i, 0), 0.2f);
        }

        if (_rooms == null || _paths == null) return;

        foreach (RoomNode room in _rooms)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawSphere(new Vector3(room.x, room.y, 0), 0.2f);
        }

        Gizmos.color = Color.yellow;
        foreach (Path path in _paths)
            Gizmos.DrawLine(new Vector3(path.room1.x, path.room1.y, 0),
                new Vector3(path.room2.x, path.room2.y, 0));
    }

    private void GenerateMap()
    {
        InitializeGrid(); // Grid 초기화
        GeneratePaths(); // 방과 경로 생성
        RemoveUnconnectedRooms(); // 연결되지 않은 방 제거
        AssignRoomTypes(); // 각 방에 타입 유형 할당
        AddBossRoom(); // 보스 방 추가
    }

    private void InitializeGrid()
    {
        for (int x = 0; x < width; x++)
        for (int y = 0; y < height; y++)
            _rooms.Add(new RoomNode(x, y));
    }

    private void GeneratePaths()
    {
        List<RoomNode> firstFloorRooms = new();

        foreach (RoomNode room in _rooms)
            if (room.x == 0)
                firstFloorRooms.Add(room);

        // 시작하는 방 2개 랜덤 선택
        RoomNode start1 = firstFloorRooms[Random.Range(0, firstFloorRooms.Count)];
        RoomNode start2;
        do
        {
            start2 = firstFloorRooms[Random.Range(0, firstFloorRooms.Count)];
        } while (start1 == start2);

        // 방들 사이의 랜덤 경로 생성
        var currentFloorRooms = new List<RoomNode> { start1, start2 };

        for (int x = 1; x < width; x++)
        {
            List<RoomNode> nextFloorRooms = new();

            foreach (RoomNode room in currentFloorRooms)
            {
                var possibleRooms = GetNextFloorRooms(room);
                if (possibleRooms.Count > 0)
                {
                    // 다음 층의 방 중에서 랜덤으로 하나 골라 경로 저장
                    //TODO: 방들 경로가 겹치지 않도록 수정하기
                    RoomNode nextRoom = possibleRooms[Random.Range(0, possibleRooms.Count)];
                    _paths.Add(new Path(room, nextRoom));
                    nextFloorRooms.Add(nextRoom);
                }
            }

            currentFloorRooms = nextFloorRooms;
        }
    }

    private List<RoomNode> GetNextFloorRooms(RoomNode room)
    {
        List<RoomNode> result = new();

        // x 좌표가 1 증가하고 y 좌표 변화량이 1 이하인 방 리턴
        foreach (RoomNode r in _rooms)
            if (r.x == room.x + 1 && Mathf.Abs(r.y - room.y) <= 1)
                result.Add(r);

        return result;
    }

    private void RemoveUnconnectedRooms()
    {
        var roomsToRemove = new List<RoomNode>();

        foreach (RoomNode r in _rooms)
        {
            bool isInPaths = false;

            foreach (Path p in _paths)
                if (p.Contains(r))
                {
                    isInPaths = true;
                    break;
                }

            if (!isInPaths) roomsToRemove.Add(r);
        }

        foreach (RoomNode r in roomsToRemove) _rooms.Remove(r);
    }

    private void AssignRoomTypes()
    {
        foreach (RoomNode room in _rooms)
            room.AssignRoomType();
    }

    private void AddBossRoom()
    {
        RoomNode bossRoom = new(width, height / 2);

        foreach (RoomNode room in _rooms)
            if (room.x == width - 1)
                _paths.Add(new Path(room, bossRoom));

        _rooms.Add(bossRoom);
    }
}

public class RoomNode
{
    //TODO: 방 타입 규칙 적용하기 (확률, 고정 생성 등)

    public enum RoomType
    {
        Shop,
        Treasure,
        Enemy,
        Rest
    }

    private RoomType _roomType;

    public int x, y;

    public RoomNode(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void AssignRoomType()
    {
        Array values = Enum.GetValues(typeof(RoomType));
        _roomType = (RoomType)values.GetValue(Random.Range(0, values.Length));
    }
}

public class Path
{
    public RoomNode room1, room2;

    public Path(RoomNode r1, RoomNode r2)
    {
        room1 = r1;
        room2 = r2;
    }

    public bool Contains(RoomNode r)
    {
        return room1 == r || room2 == r;
    }
}