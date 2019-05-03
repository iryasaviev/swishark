using Infrastructure.Entities;
using Infrastructure.Enums;
using Services.ProjectTask;
using Services.Validation;
using System.Collections.Generic;

namespace Services.Project
{
    public class ProjectHelper
    {
        Json _json;
        User _user;
        ValidationHelper _validation;
        ProjectService _service;
        Infrastructure.Entities.Project _project;
        public ProjectHelper()
        {
            _json = new Json();
            _user = new User();
            _validation = new ValidationHelper();
            _service = new ProjectService();
            _project = new Infrastructure.Entities.Project();
        }

        class Marks
        {
            public string Num { get; set; }
            public string Color { get; set; }
            public string Text { get; set; }
        }


        /// <summary>
        /// Создает новый проект.
        /// </summary>
        /// <param name="dataStr">JSON-строка данных.</param>
        /// <param name="user">Создатель проекта.</param>
        /// <returns>Возвращает код состояния результата.</returns>
        public Codes.States Create(string dataStr, User user)
        {
            Dictionary<string, string> data = _json.From(dataStr);

            _project.Name = data["Name"];
            _project.Description = data["Description"];
            _project.UserId = user.Id;

            _service.Add(_project);
            return Codes.States.Success;
        }

        /// <summary>
        /// Обновляет данные проекта.
        /// </summary>
        /// <param name="dataStr">JSON-строка данных.</param>
        /// <param name="id">Id проекта.</param>
        /// <returns>Возвращает код состояния результата.</returns>
        public Codes.States Update(string dataStr, int id)
        {
            Dictionary<string, string> data = _json.From(dataStr);
            var upProject = _service.GetProject(id);

            if (data["Form"] == "0")
            {
                upProject.Name = data["Name"];
                upProject.Description = data["Description"];
            }

            if (data["Form"] == "1")
            {
                List<object> marks = new List<object>();

                foreach (var m in data)
                    if (m.Key != "Form")
                    {
                        Marks mark = new Marks();

                        mark.Num = m.Key.Substring(4);
                        mark.Text = m.Value;

                        switch (m.Key)
                        {
                            case "Mark1":
                                mark.Color = Infrastructure.Enums.Project.Marks.Yellow.ToString();
                                break;

                            case "Mark2":
                                mark.Color = Infrastructure.Enums.Project.Marks.Orange1.ToString();
                                break;

                            case "Mark3":
                                mark.Color = Infrastructure.Enums.Project.Marks.Orange2.ToString();
                                break;

                            case "Mark4":
                                mark.Color = Infrastructure.Enums.Project.Marks.Red.ToString();
                                break;

                            case "Mark5":
                                mark.Color = Infrastructure.Enums.Project.Marks.Green1.ToString();
                                break;

                            case "Mark6":
                                mark.Color = Infrastructure.Enums.Project.Marks.Green2.ToString();
                                break;

                            case "Mark7":
                                mark.Color = Infrastructure.Enums.Project.Marks.Blue1.ToString();
                                break;

                            case "Mark8":
                                mark.Color = Infrastructure.Enums.Project.Marks.Blue2.ToString();
                                break;

                            case "Mark9":
                                mark.Color = Infrastructure.Enums.Project.Marks.Pink1.ToString();
                                break;

                            case "Mark10":
                                mark.Color = Infrastructure.Enums.Project.Marks.Pink2.ToString();
                                break;
                        }

                        marks.Add(mark);
                    }

                upProject.Marks = _json.To(marks);
            }

            try
            {
                _service.Update(upProject);
                return Codes.States.Success;
            }
            catch
            {
                // TODO: Сделать вывод ошибки.
                return Codes.States.Success;
            }
        }


        public object Get(int id)
        {
            TaskService tService = new TaskService();

            return new { project = _service.GetProject(id), tasks = tService.GetTasks(id) };
        }
    }
}