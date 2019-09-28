using System.Collections.Generic;

namespace Homework {
	internal class Student {
		public int Id { get; set; }
		public string FullName { get; set; }
		public string Speciality { get; set; }
		public int Course { get; set; }
		public ICollection<Subject> Subjects { get; set; }
	}
}