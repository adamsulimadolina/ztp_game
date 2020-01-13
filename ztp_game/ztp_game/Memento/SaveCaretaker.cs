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
                    writer.Close();
                }
            }
        }

        public SaveMemento GetMemento()
        {
            SaveMemento fileData;
            //pobranie z pliku
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
                    reader.Close();
                    fileData = new SaveMemento(levelArray, points, level, health);
                }
            }
            return memento = fileData;
        }

    }
}
