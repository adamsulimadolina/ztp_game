using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ztp_game.Logic;
using ztp_game.Sprites;

namespace ztp_game.Memento
{
    public class SaveCaretaker
    {
        private SaveMemento memento;

        public SaveCaretaker()
        {
            SaveMemento fileData;
            //pobranie z pliku
            if (File.Exists("Save"))
            {
                using (FileStream stream = new FileStream("Save", FileMode.Open))
                {
                    using (BinaryReader reader = new BinaryReader(stream))
                    {
                        char[,] levelArray = new char[Screen.getHeight(), Screen.getWidth()];
                        for (int i = 0; i < levelArray.GetLength(0); i++)
                        {
                            for (int j = 0; j < levelArray.GetLength(1); j++)
                            {
                                levelArray[i, j] = reader.ReadChar();
                            }
                        } 
                        int points = reader.ReadInt32();
                        int health = reader.ReadInt32();
                        int level = reader.ReadInt32();
                        int velocityx = reader.ReadInt32();
                        int velocityy = reader.ReadInt32();
                        float positionx = reader.ReadSingle();
                        float positiony = reader.ReadSingle();
                        int direction = reader.ReadInt32();
                        int pick = reader.ReadInt32();
                        float speed = reader.ReadSingle();
                        reader.Close();
                        fileData = new SaveMemento(levelArray, points, level, health, new Vector2(velocityx, velocityy), new Vector2(positionx, positiony), (Direction) direction, pick, speed);
                    }
                }
                memento = fileData;
            }
        }

        public void AddMemento(SaveMemento memento)
        {
            //zapis do pliku
            using (FileStream stream = new FileStream("Save", FileMode.Create))
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    var levelArray = memento.GetLevelArray();
                    for (int i = 0; i < levelArray.GetLength(0); i++)
                    {
                        for (int j = 0; j < levelArray.GetLength(1); j++)
                        {
                            writer.Write(levelArray[i, j]);
                        }
                    }
                    writer.Write(memento.GetPoints());
                    writer.Write(memento.GetHealth());
                    writer.Write(memento.GetLevel());
                    writer.Write(memento.GetVelocity().X);
                    writer.Write(memento.GetVelocity().Y);
                    writer.Write(memento.GetPosition().X);
                    writer.Write(memento.GetPosition().Y);
                    writer.Write((int)memento.GetDirection());
                    writer.Write(memento.GetPick());
                    writer.Write(memento.GetSpeed());
                    writer.Close();
                    this.memento = memento;
                }
            }
        }

        public SaveMemento GetMemento()
        {
            return memento;
        }

        public void RemoveSave()
        {
            if (File.Exists("Save"))
            {
                File.Delete("Save");
            }
        }

    }
}
