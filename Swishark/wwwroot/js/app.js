'use strict';



const ENUMS = {
    States: {
        Success: '200',

        ErrorValidationLimitMin: '4611',
        ErrorValidationLimitMax: '4631',
        ErrorValidationEmail: '461503',
        ErrorValidationPassword: '461504',

        ErrorAccountDoesNotExist: '4604',
        ErrorAccountEmailIsBusy: '4609',
        ErrorAccountIncorrectPassword: '4616'
    },

    Pages: {
        Home: '1',
        SignIn: '2',
        SignUp: '3',

        Profile: '11',

        Project: '21',
        ProjectAdd: '22'
    },

    Project: {
        Marks: {
            Yellow: 1,
            Orange1: 2,
            Orange2: 3,
            Red: 4,
            Green1: 5,
            Green2: 6,
            Blue1: 7,
            Blue2: 8,
            Pink1: 9,
            Pink2: 10,
        }
    }
};


let app = document.getElementById('app');

Object.prototype.searchParent = function (parentClass) {
    var p = this.parentNode,
        result = false;

    while (p !== document) {
        if (p.classList.contains(parentClass)) {
            result = p;
            return result;
        }
        p = p.parentNode;

        if (p === document) {
            return result;
        }
    }
};


var ajax = {

    Send: function (data, modelProperty, page) {

        var xhr = this.XHR();

        data = modelProperty + '=' + encodeURIComponent(data);

        // ('Запрос', 'Строка, метод')
        xhr.open('POST', page, false);
        xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        xhr.send(data);
    },

    Get: function (method) {
        var xhr = this.XHR();

        // ('Запрос', 'Строка, метод')
        xhr.open('GET', method, false);
        xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        xhr.send();

        if (xhr.status !== 200) {
            console.log("ERROR! Code: 'ErrorNumber' - Ajax. Ошибка сервера.");
            return xhr.status + ': ' + xhr.statusText;
        }
        else {
            return xhr.responseText;
        }
    },

    SendAndRecive: function (data, modelProperty, page) {
        var xhr = this.XHR();
        data = modelProperty + '=' + encodeURIComponent(data);

        // ('Запрос', 'Строка, метод')
        xhr.open('POST', page, false);
        xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        xhr.send(data);

        // Получаем ответ от сервера.
        xhr.onreadystatechange = function () {
            if (xhr.readyState === 4) {
                if (xhr.status === 200) {
                    return xhr.responseText;
                }
            }
        };
        // После десериализации возвращет полноценную JSON строку. Например, "result".
        try {
            return xhr.responseText;
        }
        catch{
            console.log("ERROR! Code: 'ErrorNumber' - Ajax. Ошибка сервера.");
            return xhr.responseText;
        }
    },

    SendFiles: function (file, name, page, formData) {
        // http://www.cyberforum.ru/javascript/thread1232332.html
        // https://developer.mozilla.org/en-US/docs/Web/API/FormData/Using_FormData_Objects#Sending_files_using_a_FormData_object

        var xhr = this.XHR(),
            form = new FormData();

        if (file.name !== null && file.name !== undefined) {
            form.append(name, file);
            form.append("fileData", formData);
        }
        else {
            for (var x = 0; x < file.length; x++) {
                form.append(name, file[x]);
            }
            form.append("fileData", formData);
        }
        
        xhr.open("POST", page, false);
        xhr.send(form);

        // Получаем ответ от сервера.
        xhr.onreadystatechange = function () {

            if (xhr.readyState === 4) {
                if (xhr.status === 200) {
                    return xhr.responseText;
                }
            }
		};

        // После десериализации возвращет полноценную JSON строку. Например, "result".
        try {
            return JSON.parse(xhr.responseText);
        }
        catch{

            // TODO: Расписать ошибку.
            console.log("ERROR! Code: 'ErrorNumber' - Ajax. Ошибка сервера.");

            return xhr.responseText;
        }
    },
    
    XHR: function () {
        var xmlhttp;

        try {
            xmlhttp = new ActiveXObject("Msxml2.XMLHTTP");
        }
        catch (e) {
            try {
                xmlhttp = new ActiveXObject("Microsoft.XMLHTTP");
            } catch (E) {
                xmlhttp = false;
            }
        }
        if (!xmlhttp && typeof XMLHttpRequest !== 'undefined') {
            xmlhttp = new XMLHttpRequest();
        }
        return xmlhttp;
    }
};


var convert = {
	ToJson: function(data){
		return JSON.stringify(data);
	},

    FromJson: function (data) {
        if (data === null) {
            return null;
        }
		return JSON.parse(data);
	}
};


var popUp = {
    Open: function (headText, bodyContent, func) {
        let wrapper = document.getElementById('popUp'),
            head = wrapper.getElementsByClassName('PopUpHead')[0],
            body = wrapper.getElementsByClassName('PopUpBody')[0];

        head.innerText = headText;
        body.innerHTML = bodyContent;

        if (!app.classList.contains('pp-active')) {
            app.classList.add('pp-active');
        }

        for (let btn of wrapper.getElementsByClassName('PopUpButton')) {
            btn.addEventListener('click', popUp.Close);

            if (btn.classList.contains('PopUpPerformButton')) {
                if (func !== undefined) {
                    btn.addEventListener('click', func);
                }
            }
        }
    },

    Close: function () {
        if (app.classList.contains('pp-active')) {
            app.classList.remove('pp-active');
        }
    }
};


var validation = {
    /**
    * @function validation.Email
    * @description validation for input by email type
    * 
    * @param {object} click ...
    * 
    * @returns {array} [0] {boolean} - error state, [1] {string} - error message.
    **/
    Email: function (click){
        var value = click.target.value,
		pattern = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,6})+$/,
		result = [true];

        if(value !== ''){
			if (pattern.test(value) === false) {
				result[0] = false;
				result[1] = 'Ошибка! Некорректный Email.';
			}
		}
		
        validation.OutputResult(click.target, result);
        return result;
	},

    /**
    * @function validation.Password
    * @description validation for input by password type
    * 
    * @param {object} click ...
    * 
    * @returns {array} [0] {boolean} - error state, [1] {string} - error message.
    **/
	Password: function(click){
        var value = click.target.value,
		pattern = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).*$/,
		result = [true];

        if(value !== ''){
			if (pattern.test(value) === false) {
				result[0] = false;
				result[1] = 'Ошибка! Некорректный пароль.';
			}
		}

        validation.OutputResult(click.target, result);
        return result;
	},

	OutputResult: function(item, result){
		var itemWrapper = item.searchParent('InputWrapper'),
		errorMess = itemWrapper.getElementsByClassName('ErrorMessage')[0];

		if(result[0]){
			errorMess.innerHTML = '';
			if(itemWrapper.classList.contains('fm-item--error')){
				itemWrapper.classList.remove('fm-item--error');
			}
		}
		else{
			errorMess.innerHTML = result[1];
			if(!itemWrapper.classList.contains('fm-item--error')){
				itemWrapper.classList.add('fm-item--error');
			}
		}
    },

    /**
    * @function validation.SetEvents
    * @description sets event on input by type
    **/
    SetEvents: function () {
        let items = app.getElementsByClassName('InputWrapper');

        var input,
            type;

        if (items.length > 0)
            for (let item of items) {
                if (item.classList.contains('ValidationTrue')) {
                    input = item.getElementsByClassName('inp')[0];
                    type = input.getAttribute('type');

                    switch (type) {
                        case 'email':
                            input.oninput = validation.Email;
                            break;
                        case 'password':
                            input.oninput = validation.Password;
                            break;
                    }
                }
            }
    }
};


var formData = {
    /**
    * @function formData.Build
    * @description collects form data
    * 
    * @param {object} form form with inputs
    * @param {array} exceptions array of exceptions
    * 
    * @returns {object} data of form.
    **/
    Build: function (form, exceptions) {
        let inputs = form.getElementsByClassName('inp');

        var data = {};
        for (let input of inputs) {
            if (input.hasAttribute('name')) {

                let contin = true;
                if (exceptions !== undefined) {
                    for (let exception of exceptions) {
                        if (input.getAttribute('name') === exception) {
                            contin = false;
                            break;
                        }
                    }
                }

                if (contin)
                    data[input.getAttribute('name')] = input.value;
            }
        }
        return data;
    }
};



var account = {
    /**
    * @function SignUp
    * @description отправка данных с формы регистрации
    *
    * @param {object} click submit button
    * @param {object} target is form
    * 
    * @returns {string} alert message of validation error.
    **/
    SignUp: function (click, target) {
        var form = target,
            inpWrappers = form.getElementsByClassName('InputWrapper');

        for (let a = 0; inpWrappers.length > a; a++) {
            if (inpWrappers[a].classList.contains('fm-item--error')) {
                return alert('Ошибка валидации. Пожалуйста, проверьте поля формы.');
            }
        }

        var data = formData.Build(form),
            response = ajax.SendAndRecive(convert.ToJson(data), 'Data', 'signup');

        if (response === ENUMS.States.Success) {
            // Временное способ, потом тут будет решение для SPA.
            document.location.href = '/project/add';
        }
    },

    /**
    * @function SignIn
    * @description отправка данных с формы авторизации
    * 
    * @param {object} click submit button
    * @param {object} target is form
    **/
    SignIn: function (click, target) {
        var form = target,
            data = formData.Build(form),
            response = ajax.SendAndRecive(convert.ToJson(data), 'Data', 'signin');

        if (response === ENUMS.States.Success) {
            // Временное способ, потом тут будет решение для SPA.
            document.location.href = '/project/list';
        }
    }
};



var project = {

    data: {},

    member: {
        add: {
            OpenPopUpForm: function () {
                popUp.Open('Добавление нового участника', '<div class="fm ProjectTaskEditWrapper" data-controller="task" data-method="Edit"><div class="fm-item"><label class="fm-item--lb">Идентификатор (id) участника<input class="inp ProjectMemberId" name="Id" type="number"></label></div><div class="fm-item"><label class="fm-item--lb">Роль<select class="inp ProjectMemberRole" name="Role"><option value="0">Без роли</option><option value="1">Программист</option><option value="2">Менеджер</option><option value="3">Уборщик</option></select></label></div></div>', project.member.add.ClosePopUpForm);

                let pp = document.getElementById('popUp'),
                    idInput = pp.getElementsByClassName('ProjectMemberId')[0];
            },

            ClosePopUpForm: function () {
                let wrapper = app.getElementsByClassName('ProjectTaskEditWrapper')[0],
                    data = formData.Build(wrapper),
                    response = ajax.SendAndRecive(convert.ToJson(data), 'Data', location.href[location.href.length - 1] + '/api/AddMember');

                project.member.Output();
            }
        },

        GetItem: function () {
            return ajax.Get(location.href.split('/')[location.href.split('/').length - 1] + '/api/GetMember');
        },

        GetItems: function () {
            return ajax.Get(location.href.split('/')[location.href.split('/').length - 1] + '/api/GetMembers');
        },

        Output: function () {
            let wrapper = app.getElementsByClassName('app_wr-usrs_wr')[0],
                body = wrapper.getElementsByClassName('AppUsersBody')[0];

            if (!app.classList.contains('app_wr-usrs-active')) {
                app.classList.add('app_wr-usrs-active');
            }

            for (let item of body.getElementsByClassName('AppUserItemWrapper')) {
                item.remove();
            }

            let data = convert.FromJson(project.member.GetItems());

            for (let item of data) {

                let fName = '', lName = '';

                if (item.firstName !== null)
                    fName = item.firstName;

                if (item.lastName !== null)
                    lName = item.lastName;


                body.insertAdjacentHTML('afterbegin', '<div class="app_wr-usrs_bd-item_wr app_wr-usrs_bd-item-work AppUserItemWrapper"><a href="/' + item.userId + '"><div class="app_wr-usrs_bd-item_bd"><img class="app_wr-usrs_bd-item--img"><div class="app_wr-usrs_bd-item--txt AppUserName">' + fName + ' ' + lName + '</div><div class="app_wr-usrs_bd-item-wrk AppUserTask">Выполняет:</div></div></a></div>');
            }
        }
    },

    /**
    * @function project.Add
    * @description отправка данных с формы добавления проекта
    * 
    * @param {object} click submit button
    * @param {object} target is form
    **/
    Add: function (click, target) {
        let form = target,
            data = formData.Build(form),
            response = ajax.SendAndRecive(convert.ToJson(data), 'Data', 'api/AddItem');
    },

    Get: function () {
        var url = location.pathname.split('/'),
            dataTo = {
                id: url[url.length - 1]
            },
            response = ajax.SendAndRecive(convert.ToJson(dataTo), 'Data', 'api/GetItem');

        let data = project.data = convert.FromJson(response);
        if (data !== null) {
            var body = app.getElementsByClassName('TasksBody')[0],
                name = app.getElementsByClassName('TaskProjectName')[0],
                description = app.getElementsByClassName('TaskProjectDescription')[0];

            name.innerText = data.project.name;
            description.innerText = data.project.description;

            for (let a = 0; data.tasks.length > a; a++) {
                body.insertAdjacentHTML('afterbegin', '<div class="tsks-item Task"><div class="tsks-item-ttl"><div class="tsks-item-ttl--nm TaskName">' + data.tasks[a].title + '</div><div class="tsks-item-ttl--dt TaskDateTime">Выполнить до ' + new Date(data.tasks[a].finishDate).toLocaleString() + '</div></div><div class="tsks-item--dsc TaskDescription">' + data.tasks[a].description + '</div><div class="tsks-item_ft"><div class="tsks-item_ft-usrs TaskUsers"></div><div class="tsks-item--mrks TaskMarks"></div></div><input class="ds-n TaskId" value="' + data.tasks[a].id + '"></div>');

                if (data.tasks[a].marks !== null) {
                    for (let mark of convert.FromJson(data.tasks[a].marks)) {
                        body.getElementsByClassName('Task')[0].getElementsByClassName('TaskMarks')[0].insertAdjacentHTML('beforeend', '<div class="tsks-item--mrk ' + mark.Color + '">' + mark.Text + '<div>');
                    }
                }
            }
        }
    },

    GetItems: function () {
        var url = location.pathname.split('/'),
            dataTo = {
                id: url[url.length - 1]
            },
            response = ajax.SendAndRecive(convert.ToJson(dataTo), 'Data', 'api/GetItems');

        let data = convert.FromJson(response);
        if (data !== null) {
            project.data = data;

            var body = app.getElementsByClassName('ProjectListBody')[0];

            for (let a = 0; data.length > a; a++) {
                body.insertAdjacentHTML('afterbegin', '<div class="pr_bd-list--item ProjectListItem"><div class="pr_bd-list--item_hd"><div class="pr_bd-list--item_hd-nm ProjectListItemName">' + data[a].name + '</div><div class="pr_bd-list--item_hd-dsc ProjectListItemDescription">' + data[a].description + '</div></div><div class="pr_bd-list--item_ft"><a href="' + data[a].id + '"><div class="btn pr_bd-list--item_ft--btn">Открыть</div></a></div></div>');
            }
        }
    },
        
    Update: function (click) {
        let form = click.searchParent('fm'),
            data = formData.Build(form);

        if (form.classList.contains('ProjectSettingsDataWrapper'))
            data['Form'] = 0;

        if (form.classList.contains('ProjectSettingsMarksWrapper'))
            data['Form'] = 2;

        let response = ajax.SendAndRecive(convert.ToJson(data), 'Data', '/project/' + project.data.project.id + '/api/Update');
    },

    UpdateRoles: function () {
        let data = settings.project.memberRole.Build();

        let response = ajax.SendAndRecive(convert.ToJson(data), 'Data', '/project/' + project.data.project.id + '/api/UpdateRoles');
    }
};



var settings = {
    Account: function () {

    },

    project: {
        GetItem: function () {
            let data = { id: location.pathname.split('/')[location.pathname.split('/').length - 1] },
                response = ajax.SendAndRecive(convert.ToJson(data), 'Data', '/project/api/GetItem');

            project.data = data = convert.FromJson(response);

            let dataWr = app.getElementsByClassName('ProjectSettingsDataWrapper')[0];

            dataWr.getElementsByClassName('ProjectSettingsName')[0].value = data.project.name;
            dataWr.getElementsByClassName('ProjectSettingsDescription')[0].innerText = data.project.description;

            let marksWr = app.getElementsByClassName('ProjectSettingsMarksWrapper')[0],
                mItems = marksWr.getElementsByClassName('ProjectSettingsMark');

            if (data.project.marks !== null) {
                var marks = convert.FromJson(data.project.marks);

                for (let a = 0; mItems.length > a; a++) {
                    for (let mark of marks) {
                        if (mItems[a].classList.contains(mark.Color)) {
                            mItems[a].value = mark.Text;

                            break;
                        }
                    }
                }
            }
        },

        memberRole: {
            data: {},

            Add: function () {
                let wrapper,
                    body,
                    color = this.classList[2];

                if (event.keyCode === 32 || event.keyCode === 13) {
                    wrapper = app.getElementsByClassName('ProjectSettingsMembersRolesWrapper')[0];
                    body = wrapper.getElementsByClassName('ProjectSettingsMembersRoles')[0];
                    
                    body.insertAdjacentHTML('beforeend', '<div class="pr_stg-rls--item ProjectSettingsMembersRole ' + color + '"><span class="ProjectSettingsMembersRoleTxt">' + this.value + '</span><div class="pr_stg-rls--item--btn ProjectSettingsMembersRoleDelBtn"></div><input class="ds-n ProjectSettingsMembersRoleId" value"" /></div>');

                    this.value = '';
                }
            },

            GetItems: function () {

            },

            ColorChoose: function (click) {
                click = click.target;

                let wrapper = app.getElementsByClassName('ProjectSettingsMembersRolesWrapper')[0],
                    input = wrapper.getElementsByClassName('ProjectSettingsUserRole')[0];

                for (let a = 0; input.classList.length > a; a++) {
                    if (input.classList[a] !== 'inp' && input.classList[a] !== 'ProjectSettingsUserRole') {
                        input.classList.remove(input.classList[a]);
                    }
                }

                input.classList.add(click.classList[2]);
            },

            Build: function () {
                let body = app.getElementsByClassName('ProjectSettingsMembersRoles')[0],
                    items = body.getElementsByClassName('ProjectSettingsMembersRole');

                let roles = [];
                for (let item of items) {
                    let role = {
                        Id: item.getElementsByClassName('ProjectSettingsMembersRoleId')[0].value,
                        Name: item.getElementsByClassName('ProjectSettingsMembersRoleTxt')[0].innerText,
                        Color: item.classList[2]
                    };
                    roles[roles.length] = role;
                }

                return roles;
            }
        }
    }
};



var task = {

    settings: {
        current: {},

        OpenForm: function (click) {
            var taskItem = click.target.searchParent('Task');

            for (let item of project.data.tasks) {
                if (item.id === Number(taskItem.getElementsByClassName('TaskId')[0].value)) {
                    task.settings.current = item;
                    break;
                }
            }

            popUp.Open('Редактирование задачи',
                '<div class="fm ProjectTaskEditWrapper" data-controller="task" data-method="Edit"><div class="fm-item"><label class="fm-item--lb">Заголовок задачи<input class="inp TaskSettingsTitle" name="Title" type="text" value="' + task.settings.current.title + '"></label></div><div class="fm-item"><div class="fm-item-l"><label class="fm-item--lb">Состояние<select class="inp TaskSettingsState" name="State"><option value="0">Не выполнено</option><option value="1">Выполнено</option></select></label></div><div class="fm-item-r"><label class="fm-item--lb">Дата окончания<input class="inp TaskSettingsFinishDate" name="FinishDate" type="datetime-local" value="' + task.settings.current.finishDate + '"></label></div></div><div class="fm-item"><label class="fm-item--lb">Описание<textarea class="inp fm-item--tta TaskSettingsDescription" name="Description">' + task.settings.current.description + '</textarea></label></div><div class="fm-item tsk_stg-item ProjectTaskMarksWrapper"><label class="fm-item--lb">Mark</label><div class="tsk_stg-item--mrks"><div class="tsk_stg-item--mrks_bd tsk_stg-item--mrks_bd1"><input class="inp tsk_stg-item--mrk TaskMark Yellow" name="Mark1" type="text" readonly><input class="inp tsk_stg-item--mrk TaskMark Orange1" name="Mark2" type="text" readonly><input class="inp tsk_stg-item--mrk TaskMark Orange2" name="Mark3" type="text" readonly><input class="inp tsk_stg-item--mrk TaskMark Red" name="Mark4" type="text" readonly><input class="inp tsk_stg-item--mrk TaskMark Pink1" name="Mark9" type="text" readonly></div><div class="tsk_stg-item--mrks_bd tsk_stg-item--mrks_bd2"><input class="inp tsk_stg-item--mrk TaskMark Green1" name="Mark5" type="text" readonly><input class="inp tsk_stg-item--mrk TaskMark Green2" name="Mark6" type="text" readonly><input class="inp tsk_stg-item--mrk TaskMark Blue1" name="Mark7" type="text" readonly><input class="inp tsk_stg-item--mrk TaskMark Blue2" name="Mark8" type="text" readonly><input class="inp tsk_stg-item--mrk TaskMark Pink2" name="Mark10" type="text" readonly></div></div></div><input class="ds-n TaskEditId" value="' + task.settings.current.id + '"></div>',
                task.settings.CloseForm);

            let wrapper = app.getElementsByClassName('ProjectTaskEditWrapper')[0],
                marks = wrapper.getElementsByClassName('TaskMark');

            wrapper.getElementsByClassName('TaskSettingsState')[0].value = task.settings.current.state;

            for (let mark of marks) {

                // Перебор по маркам проекта.
                let pMarks = convert.FromJson(project.data.project.marks);
                for (let pMark of pMarks) {
                    if (mark.classList.contains(pMark.Color)) {
                        mark.value = pMark.Text;

                        break;
                    }
                }

                // Перебор по маркам задачи.
                let tMarks = convert.FromJson(task.settings.current.marks);
                if (tMarks !== null) {
                    for (let tMark of tMarks) {
                        if (mark.classList.contains(tMark.Color)) {
                            mark.classList.add('tsk_stg-item--mrk-active');

                            break;
                        }
                    }
                }

                mark.addEventListener('click', task.settings.ChoicheMark);
            }
        },

        CloseForm: function () {
            let wrapper = app.getElementsByClassName('ProjectTaskEditWrapper')[0],
                data = {},
                fData = formData.Build(wrapper);

            let newMarks = [],
                marks = wrapper.getElementsByClassName('TaskMark');

            // Сбор выделенных марок.
            for (let mark of marks) {
                if (mark.classList.contains('tsk_stg-item--mrk-active')) {

                    let prMarks = convert.FromJson(project.data.project.marks);
                    for (let prMark of prMarks) {
                        if (prMark.Num === Number(mark.getAttribute('name').substring(4))) {
                            newMarks[newMarks.length] = prMark;
                        }
                    }
                }
            }

            data["Title"] = task.settings.current.title = fData.Title;
            data["Description"] = task.settings.current.description = fData.Description;
            data["State"] = task.settings.current.state = Number(fData.State);
            data["FinishDate"] = task.settings.current.finishDate = fData.FinishDate;
            data["Marks"] = task.settings.current.marks = convert.ToJson(newMarks);

            // Обновление задачи в свойстве проекта.
            for (let pTask of project.data.tasks) {
                if (pTask.id === task.settings.current.id) {
                    pTask = task.settings.current;
                    break;
                }
            }

            // Обновление данных задачи на доске.
            for (let item of app.getElementsByClassName('Task')) {
                if (Number(item.getElementsByClassName('TaskId')[0].value) === task.settings.current.id) {

                    item.getElementsByClassName('TaskName')[0].innerText = data.Title;
                    item.getElementsByClassName('TaskDateTime')[0].innerText ='Выполнить до' + new Date(data.FinishDate).toLocaleString();
                    item.getElementsByClassName('TaskDescription')[0].innerText = data.Description;

                    let mWrapper = item.getElementsByClassName('TaskMarks')[0];
                    mWrapper.innerHTML = '';
                    for (let mark of newMarks) {
                        mWrapper.insertAdjacentHTML('beforeend', '<div class="tsks-item--mrk ' + mark.Color + '">' + mark.Text + '<div>');
                    }

                    break;
                }
            }

            let response = ajax.SendAndRecive(convert.ToJson(data), 'Data', '/task/' + task.settings.current.id + '/api/Update');
        },

        ChoicheMark: function (click) {
            click.target.classList.toggle('tsk_stg-item--mrk-active');
        }
    },

    add: {
        OpenForm: function () {
            popUp.Open('Добавление задачи',
                '<div class="fm" data-controller="task" data-method="Add"><div class="fm-item InputWrapper"><label class="fm-item--lb">Заголовок задачи<input class="inp" name="Title" type="text"></label></div><div class="fm-item tsk_stg-item InputWrapper"><label class="fm-item--lb">Дата окончания<input class="inp" name="FinishDate" type="datetime-local"></label></div><div class="fm-item InputWrapper"><label class="fm-item--lb">Описание<textarea class="inp fm-item--tta" name="Description"></textarea></label></div></div>',
                task.add.CloseForm);
        },

        CloseForm: function () {
            let form = document.getElementById('popUp').getElementsByClassName('fm')[0],
                data = formData.Build(form);

            data['ProjectId'] = location.pathname.split('/')[location.pathname.split('/').length - 1];

            let response = ajax.SendAndRecive(convert.ToJson(data), 'Data', '/task/api/AddItem');

            if (response === ENUMS.States.Success) {
                var body = app.getElementsByClassName('TasksBody')[0];

                body.insertAdjacentHTML('afterbegin', '<div class="tsks-item Task"><div class="tsks-item-ttl"><div class="tsks-item-ttl--nm TaskName">' + data.Title + '</div><div class="tsks-item-ttl--dt TaskDateTime">' + new Date(data.CreatedDate).toLocaleString() + ' — ' + new Date(data.FinishDate).toLocaleString() + '</div></div><div class="tsks-item--dsc TaskDescription">' + data.Description + '</div><div class="tsks-item_ft"><div class="tsks-item_ft-usrs TaskUsers"></div><div class="tsks-item--mrks TaskMarks"></div></div></div>');
            }
        }
    }
};



document.addEventListener("DOMContentLoaded", function () {
    validation.SetEvents();
});