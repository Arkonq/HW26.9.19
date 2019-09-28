using System;
using System.Collections.Generic;
using System.Xml;

namespace Homework {
	class Program {
		public static void Problem_01() {
			/*
			1.	Прочитать содержимое XML файла со списком последних новостей по ссылке https://habrahabr.ru/rss/interesting/
					Создать класс Item со свойствами: Title, Link, Description, PubDate.
					Создать коллекцию типа List<Item> и записать в нее данные из файла.
			*/
			List<Item> items = new List<Item>();

			XmlDocument document = new XmlDocument();
			document.Load(@"https://habr.com/ru/rss/interesting/");

			XmlElement rootElement = document.DocumentElement as XmlElement;  // Получаем элемент <rss xmlns:dc="http://purl.org/dc/elements/1.1/" version="2.0">
			XmlElement rootElementChild = rootElement.FirstChild as XmlElement; // Получаем его дочерний элемент <channel> , содержащий все данные
			foreach (XmlElement channelElement in rootElementChild.ChildNodes) {  // Считываем все дочерние элементы
				XmlElement titleElement = channelElement.GetElementsByTagName("title")[0] as XmlElement;
				XmlElement linkElement = channelElement.GetElementsByTagName("link")[0] as XmlElement;
				XmlElement descriptionElement = channelElement.GetElementsByTagName("description")[0] as XmlElement;
				XmlElement pubDateElement = channelElement.GetElementsByTagName("pubDate")[0] as XmlElement;
				if (titleElement != null && linkElement != null && descriptionElement != null && pubDateElement != null) { // Так как среди дочерних элементов бывают не только элементы <item>, делаем проверку на необходимые нам данные
					items.Add(new Item {
						Title = titleElement.InnerText,
						Link = linkElement.InnerText,
						Description = descriptionElement.InnerText,
						PubDate = pubDateElement.InnerText
					});                                           // Проверка наличия данных (если понадобится)
																												//Console.WriteLine(titleElement.InnerText);
																												//Console.WriteLine(linkElement.InnerText);
																												//Console.WriteLine(descriptionElement.InnerText);
																												//Console.WriteLine(pubDateElement.InnerText);
																												//Console.WriteLine();
				}
			}
		}
		public static void Problem_02() {
			/*
			2.	С помощью класса XmlDocument создать класс который будет описывать 
					студента и вывести его на консоль или сохранить в текстовый файл
			*/
			//XML_Read_Demo()
			var student = new Student {
				Id = 1,
				FullName = "Felix Kjellberg",
				Speciality = "Linguist",
				Course = 3,
				Subjects = new List<Subject>
				{
					new Subject
					{
						Id = 1,
						Name = "Kazakh Language",
						Teacher = "Zhabyl Abilay"
					},
					new Subject
					{
						Id = 2,
						Name = "Russian Literature",
						Teacher = "Romanova Anna"
					},
				}
			};

			var document = new XmlDocument();
			var declaration = document.CreateXmlDeclaration("1.0", "utf-8", string.Empty);

			var rootElement = document.CreateElement("student");

			var studentIdElement = document.CreateElement("id");
			var studentFullNameElement = document.CreateElement("fullName");
			var studentSpecialityElement = document.CreateElement("speciality");
			var studentCourseElement = document.CreateElement("course");
			var studentSubjectsElement = document.CreateElement("subjects");

			studentIdElement.InnerText = student.Id.ToString();
			studentFullNameElement.InnerText = student.FullName;
			studentSpecialityElement.InnerText = student.Speciality;
			studentCourseElement.InnerText = student.Course.ToString();

			foreach (var subject in student.Subjects) {
				var subjectElelement = document.CreateElement("subject");

				var subjectIdElement = document.CreateElement("id");
				var subjectNameElement = document.CreateElement("name");
				var subjectTeacherElement = document.CreateElement("teacher");

				subjectIdElement.InnerText = subject.Id.ToString();
				subjectNameElement.InnerText = subject.Name;
				subjectTeacherElement.InnerText = subject.Teacher;

				subjectElelement.AppendChild(subjectIdElement);
				subjectElelement.AppendChild(subjectNameElement);
				subjectElelement.AppendChild(subjectTeacherElement);

				studentSubjectsElement.AppendChild(subjectElelement);
			}
			rootElement.AppendChild(studentIdElement);
			rootElement.AppendChild(studentFullNameElement);
			rootElement.AppendChild(studentSpecialityElement);
			rootElement.AppendChild(studentCourseElement);
			rootElement.AppendChild(studentSubjectsElement);

			document.AppendChild(declaration);
			document.AppendChild(rootElement);

			document.Save("student.xml");
		}

		static void Main() {
			Problem_01();
			Problem_02();
			Console.WriteLine("Done!");
		}
	}
}
