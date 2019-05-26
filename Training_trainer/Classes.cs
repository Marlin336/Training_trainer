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
	public class GroupList
	{
		public int id { get; set; }
		public string trainer { get; set; }
		public string min_age { get; set; }
		public string max_age { get; set; }
		public int cost { get; set; }
		public int sub { get; set; }
		public GroupList(int id, string trainer, string min_age, string max_age, int cost, int sub)
		{
			this.id = id;
			this.trainer = trainer;
			this.min_age = min_age;
			this.max_age = max_age;
			this.cost = cost;
			this.sub = sub;
		}
	}
}
