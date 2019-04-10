'use strict';

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

    Get: function (modelProperty, method) {

        var xhr = this.XHR();

        //data = modelProperty + '=' + encodeURIComponent(data);

        // ('Запрос', 'Строка, метод')
        xhr.open('GET', method);
        xhr.setRequestHeader('Content-Type', 'XMLHttpRequest');
        xhr.send();

        if (xhr.status !== 200) {
            // обработать ошибку
            alert(xhr.status + ': ' + xhr.statusText); // пример вывода: 404: Not Found
        } else {
            // вывести результат
            alert(xhr.responseText); // responseText -- текст ответа.
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
        }

        if (target.classList.contains('PopUpPerformButton')) {
        }

        if (app.classList.contains('pp-active')) {
            app.classList.remove('pp-active');
        }
    }
};



let validation = {
	Email: function(el){
		var value = el.target.value,
		pattern = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,6})+$/,
		result = [true];

        if(value !== ''){
			if (pattern.test(value) === false) {
				result[0] = false;
				result[1] = 'Ошибка! Некорректный Email.';
			}
		}
		
		validation.OutputResult(el.target, result);
        return result;
	},

	Password: function(el){
		var value = el.target.value,
		pattern = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\s).*$/,
		result = [true];

        if(value !== ''){
			if (pattern.test(value) === false) {
				result[0] = false;
				result[1] = 'Ошибка! Некорректный пароль.';
			}
		}

		validation.OutputResult(el.target, result);
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
	}
};



/**
 * @function SignUp
 * @description отправка данных с формы регистрации
 * 
 * @returns {string} alert message of validation error.
 **/
function SignUp(){
	var form = document.getElementById('form'),
	inpWrappers = form.getElementsByClassName('InputWrapper'),
	input;

	var formData = {
		FirstName:null,
		Email:null,
		Password:null
	};

	for(let a = 0; inpWrappers.length > a; a++){
		if(inpWrappers[a].classList.contains('fm-item--error')){
			return alert('Ошибка валидации. Пожалуйста, проверьте поля формы.')
		}

		input = inpWrappers[a].getElementsByClassName('inp')[0];

		switch(input.getAttribute('name')){
			case 'FirstName':
				formData.FirstName = input.value;
			break;

			case 'Email':
				formData.Email = input.value;
			break;

			case 'Password':
				formData.Password = input.value;
			break;
		}
	}

    var response = ajax.SendAndRecive(convert.ToJson(formData), 'Data', 'signup');
    switch (response) {
        case '200':
            page.setUrl('Создание проекта', '/project/add');
            page.setName('31');
            page.setData();
            page.setHandlers();
            break;
    }
}

/**
 * @function SignIn
 * @description отправка данных с формы авторизации.
 **/
function SignIn() {
    var form = document.getElementById('form'),
        inpWrappers = form.getElementsByClassName('InputWrapper'),
        input;

    var formData = {
        Email: null,
        Password: null
    };

    for (let a = 0; inpWrappers.length > a; a++) {
        input = inpWrappers[a].getElementsByClassName('inp')[0];

        switch (input.getAttribute('name')) {
            case 'Email':
                formData.Email = input.value;
                break;

            case 'Password':
                formData.Password = input.value;
                break;
        }
    }

    var response = ajax.SendAndRecive(convert.ToJson(formData), 'Data', 'signin');
    switch (response) {
        case '200':
            page.setUrl('Создание проекта', '/project/add');
            page.setName('31');
            page.setData();
            page.setHandlers();
            break;
    }
}



var project = {
    /**
    * @function ProjectAdd
    * @description отправка данных с формы добавления проекта
    **/
    Add: function () {
        var form = document.getElementById('form'),
            inpWrappers = form.getElementsByClassName('InputWrapper'),
            input;

        var formData = {
            Name: null,
            City: null,
            TryGetVisible: false,
            SquareType: null
        };

        for (let a = 0; inpWrappers.length > a; a++) {
            input = inpWrappers[a].getElementsByClassName('inp')[0];

            switch (input.getAttribute('name')) {
                case 'Name':
                    formData.Name = input.value;
                    break;

                case 'City':
                    formData.City = input.value;
                    break;

                case 'TryGetVisible':
                    formData.TryGetVisible = input.value;
                    break;
                case 'SquareType':
                    formData.SquareType = input.value;
                    break;
            }
        }

        var response = ajax.SendAndRecive(convert.ToJson(formData), 'Data', 'add');
    }
};



document.addEventListener("DOMContentLoaded", function () {
});