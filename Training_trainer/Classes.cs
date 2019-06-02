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
	public class CustomerList
	{
		public int id { get; set; }
		public string sname { get; set; }
		public string fname { get; set; }
		public string pname { get; set; }
		public int age { get; set; }
		public string mail { get; set; }
		public CustomerList(int id, string sname, string fname, string pname, int age, string mail)
		{
			this.id = id;
			this.sname = sname;
			this.fname = fname;
			this.pname = pname;
			this.age = age;
			this.mail = mail;
		}
	}
}
