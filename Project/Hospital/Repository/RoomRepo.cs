using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Repository
{
   public class RoomRepo
   {
      private String dbPath;
        public List<Room> Rooms { get; set; }
      
      public RoomRepo(string db_path, List<Room> rooms)
        {
            this.dbPath = db_path;
            this.Rooms = rooms;
            Room room1 = new Room("1A", 1, 1, true, "sala6");
            Room room2 = new Room("3A", 1, 1, true, "soba6");

            this.Rooms.Add(room1);
            this.Rooms.Add(room2);
        }
      
      public bool NewRoom(Room room)
      {  
         // logic for when you cant add room
         Rooms.Add(room);
         return true;
      }
      
      public Room GetRoom(String roomId)
      {
         foreach(Room r in Rooms)
                if(r.Id.Equals(roomId))
                    return r;

         return null;
      }
      
      public void SetRoom(String roomId, Room newRoom)
      {
            int idx = Rooms.FindIndex(r => r.Id.Equals(roomId));
            Rooms[idx] = new Room(newRoom);
      }
      
      public bool DeleteRoom(String roomId)
      {
         foreach (Room r in Rooms)
                if (r.Id.Equals(roomId))
                {
                    Rooms.Remove(r);
                    return true;
                }
         return false;
      }
      
      public bool LoadRoom()
      {
         // logic for deserialization
         throw new NotImplementedException();
      }
      
      public bool SaveRoom()
      {  
         // logic for serialization
         throw new NotImplementedException();
      }

    }
}