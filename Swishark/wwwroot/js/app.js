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


//var page = {
//    GetNum: function () {
//        return document.getElementById('pageNum');
//    }
//};


var ajax = {

    Send: function (data, modelProperty, page) {

        var xhr = this.XHR();

        data = modelProperty + '=' + encodeURIComponent(data);

        // ('Запрос', 'Строка, метод')
        xhr.open('POST', page, false);
        xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');
        xhr.send(data);
    },

    Get: function (modelProperty, method) {

        var xhr = this.XHR();

        //data = modelProperty + '=' + encodeURIComponent(data);

        // ('Запрос', 'Строка, метод')
        xhr.open('GET', method);
        xhr.setRequestHeader('Content-Type', 'XMLHttpRequest');
        xhr.send();

        if (xhr.status !== 200) {
            // обработать ошибку
            //alert(xhr.status + ': ' + xhr.statusText); // пример вывода: 404: Not Found
        } else {
            // вывести результат
            //alert(xhr.responseText); // responseText -- текст ответа.
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

	FromJson: function(data){
		return JSON.parse(data);
	}
};


var popUp = {
    Open: function (headText, bodyContent) {
        let wrapper = document.getElementById('popUp'),
            head = wrapper.getElementsByClassName('PopUpHead')[0],
            body = wrapper.getElementsByClassName('PopUpBody')[0];

        head.innerText = headText;
        body.innerHTML = bodyContent;

        if (!app.classList.contains('pp-active')) {
            app.classList.add('pp-active');
        }

        for (let btn of wrapper.getElementsByClassName('PopUpButton')) {
            btn.onclick = popUp.Close;
        }

        wrapper.getElementsByClassName('PopUpCloseButton')[0].onclick = popUp.Close;
    },

    Close: function (click) {
        let target = click.target;

        if (target.classList.contains('PopUpCloseButton')) {
            //...
        }

        if (target.classList.contains('PopUpPerformButton')) {
            //...
        }

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
            document.location.href = '/project/add';
        }
    }
};



var project = {
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
            response = ajax.SendAndRecive(convert.ToJson(data), 'Data', 'add');
    },

    Get: function () {
        var url = location.pathname.split('/'),
            dataTo = {
                id: url[url.length - 1]
            },
            response = ajax.SendAndRecive(convert.ToJson(dataTo), 'Data', 'api/GetItem');

        let data = convert.FromJson(response);
        if (data !== null) {
            var name = app.getElementsByClassName('TaskProjectName')[0],
                description = app.getElementsByClassName('TaskProjectDescription')[0];

            name.innerText = data['name'];
            description.innerText = data['description'];
        }
    }
};



document.addEventListener("DOMContentLoaded", function () {
    validation.SetEvents();
});