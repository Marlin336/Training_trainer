using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training_trainer
{
    public class ExerciseList
    {
		public int id { get; set; }
		public string name { get; set; }
		public ExerciseList(int id, string name)
		{
			this.id = id;
			this.name = name;
		}
    }
}
