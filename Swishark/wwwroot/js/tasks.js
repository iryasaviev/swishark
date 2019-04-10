'use strict';



var task = {
    add: {
        OpenSettings: function (click) {
            let wrapper = document.getElementById('tasksWrapper');

            wrapper.classList.toggle('tsk_stg-active');
        },

        OpenPopUpForm: function () {
            popUp.Open('Добавление задачи', '<div class="fm"><div class="fm-item InputWrapper"><label class="fm-item--lb" for="taskTitle">Task title</label><input class="inp" id="taskTitle" name="Title" type="text"></div><div class="fm-item InputWrapper"><label class="fm-item--lb" for="taskDescription">Description</label><textarea class="inp fm-item--tta" id="taskDescription" name="Description"></textarea></div></div>');
        }
    }
};



document.addEventListener("DOMContentLoaded", function () {
    if (document.getElementById('tasksWrapper').getElementsByClassName('Task') !== null) {
        let items = document.getElementById('tasksWrapper').getElementsByClassName('Task');
        for (let item of items) {
            item.onclick = task.add.OpenSettings;
        }
    }

    if (document.getElementById('tasksWrapper').getElementsByClassName('TaskAddBtn') !== null) {
        document.getElementById('tasksWrapper').getElementsByClassName('TaskAddBtn')[0].onclick = task.add.OpenPopUpForm;
    }
});