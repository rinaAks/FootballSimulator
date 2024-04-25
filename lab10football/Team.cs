using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab10football
{
    public class Team
    {
        public string Name { get; set; }
        
        /// <summary>
        /// Среднее количество голов команды
        /// (нужно для Пуассоновского распределения)
        /// </summary>
        public double Lyambda { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lyambda"></param>
        public Team(string name, double lyambda)
        {
            Name = name;
            Lyambda = lyambda;
        }
    }
}
