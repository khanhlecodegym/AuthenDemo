using System.Collections.Generic;

namespace AuthenDemo.Models
{
	public class UserDBFake
	{
		public static List<UserModel> Users = new List<UserModel>()
		{
			new UserModel() { UserName = "KhanhLQ", Email = "khanhlq@test1", Password = "123456789", Role = "Administrator" },
			new UserModel() { UserName = "Test01", Email = "user01@test01", Password = "123456789", Role = "Partner" },
		};
	}
}
