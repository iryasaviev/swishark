using Infrastructure.Entities;
using Infrastructure.Enums;
using Services.Account;
using Services.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Settings
{
    public class SettingsHelper
    {
        User _user;
        Infrastructure.Entities.Project _project;

        Json _json;
        ValidationHelper _validation;
        SettingsService _service;
        public SettingsHelper()
        {
            _user = new User();
            _project = new Infrastructure.Entities.Project();

            _json = new Json();
            _validation = new ValidationHelper();
            _service = new SettingsService();
        }

        class Marks
        {
            public string Num { get; set; }
            public string Color { get; set; }
            public string Text { get; set; }
        }



        /// <summary>
        /// Обновляет данные проекта.
        /// </summary>
        /// <param name="dataStr">JSON-строка данных.</param>
        /// <returns></returns>
        public Codes.States ProjectUpdate(string dataStr, User user)
        {
            Dictionary<string, string> data = _json.From(dataStr);

            if (data["Form"] == "0")
            {
                _project.Name = data["Name"];
                _project.Description = data["Description"];
                _project.UserId = user.Id;
            }

            if (data["Form"] == "1")
            {
                List<object> marks = new List<object>();
                Marks mark = new Marks();

                foreach (var m in data)
                    if(m.Key != "Form")
                    {
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

                _project.Marks = _json.To(marks);
            }

            _service.UpdateProject(_project);
            return Codes.States.Success;
        }
    }
}