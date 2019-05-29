using Infrastructure.Entities;
using Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Mark
{
    public class MarkHelper
    {
        Json _json;
        MarkService _service;
        public MarkHelper()
        {
            _json = new Json();
            _service = new MarkService();
        }

        class Mark
        {
            public string Marks { get; set; }
            public int Num { get; set; }
            public string Color { get; set; }
            public string Text { get; set; }
            public bool Active { get; set; }
        }


        /// <summary>
        /// Создает марки проекта.
        /// </summary>
        /// <returns>Код состояния.</returns>
        public Codes.States CreateProjectMark(int projectId)
        {
            ProjectMark pMark = new ProjectMark();
            for (int i = 1; 10 >= i; i++)
            {
                pMark.Id = Guid.NewGuid();
                pMark.Num = i;
                pMark.ProjectId = projectId;

                if (i == (int)Infrastructure.Enums.Project.Marks.Yellow)
                    pMark.Color = Infrastructure.Enums.Project.Marks.Yellow.ToString();

                if (i == (int)Infrastructure.Enums.Project.Marks.Orange1)
                    pMark.Color = Infrastructure.Enums.Project.Marks.Orange1.ToString();

                if (i == (int)Infrastructure.Enums.Project.Marks.Orange2)
                    pMark.Color = Infrastructure.Enums.Project.Marks.Orange2.ToString();

                if (i == (int)Infrastructure.Enums.Project.Marks.Red)
                    pMark.Color = Infrastructure.Enums.Project.Marks.Red.ToString();

                if (i == (int)Infrastructure.Enums.Project.Marks.Green1)
                    pMark.Color = Infrastructure.Enums.Project.Marks.Green1.ToString();

                if (i == (int)Infrastructure.Enums.Project.Marks.Green2)
                    pMark.Color = Infrastructure.Enums.Project.Marks.Green2.ToString();

                if (i == (int)Infrastructure.Enums.Project.Marks.Blue1)
                    pMark.Color = Infrastructure.Enums.Project.Marks.Blue1.ToString();

                if (i == (int)Infrastructure.Enums.Project.Marks.Blue2)
                    pMark.Color = Infrastructure.Enums.Project.Marks.Blue2.ToString();

                if (i == (int)Infrastructure.Enums.Project.Marks.Pink1)
                    pMark.Color = Infrastructure.Enums.Project.Marks.Pink1.ToString();

                if (i == (int)Infrastructure.Enums.Project.Marks.Pink2)
                    pMark.Color = Infrastructure.Enums.Project.Marks.Pink2.ToString();

                _service.AddToProject(pMark);
            }

            return Codes.States.Success;
        }

        /// <summary>
        /// Обновляет марки проекта.
        /// </summary>
        /// <param name="dataStr"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Codes.States UpdateProjectMark(string dataStr, int projectId)
        {
            Dictionary<string, string> data = _json.From(dataStr);

            ProjectMark pMark = new ProjectMark();
            List<ProjectMark> marks = _service.GetItemsOnProject(projectId);

            int i = 1;
            foreach (var mark in marks)
            {
                mark.Text = data["Mark" + i];
                _service.UpdateInProject(mark);

                i++;
            }

            return Codes.States.Success;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataStr"></param>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public Codes.States CreateTaskMark(string dataStr, int projectId, Guid taskId)
        {
            List<Mark> data = _json.From1<Mark>(dataStr);
            
            TaskMark mark = new TaskMark();


            // TODO: Доделать!
            for (int i = 0; data.Count > i; i++)
            {
                foreach (var pMark in _service.GetItemsOnProject(projectId))
                {
                    if (pMark.Num == i + 1)
                    {
                        mark.Id = Guid.NewGuid();
                        mark.ProjectMarkId = pMark.Id;
                        mark.Active = data[i].Active;
                        mark.TaskId = taskId;

                        _service.AddToTask(mark);
                        break;
                    }
                }
            }

            return Codes.States.Success;
        }

        /// <summary>
        /// Обновляет марки задачи.
        /// </summary>
        /// <param name="dataStr"></param>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public Codes.States UpdateTaskMark(string dataStr, Guid taskId)
        {
            TaskMark tMark = new TaskMark();

            Dictionary<string, string> data = _json.From(dataStr);
            List<TaskMark> marks = _service.GetItemsOnTasks(taskId);
            if (marks != null)
            {
                foreach (var item in data)
                {
                    foreach (TaskMark mark in _service.GetItemsOnTasks(taskId))
                    {

                    }
                }
            }
            return Codes.States.Success;
        }
    }
}
