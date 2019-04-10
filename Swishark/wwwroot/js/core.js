/**
 * 
 * @author Ilnur Yasaviev <ir_yasaviev>
 * @copyright ProjectName, 2019
 * 
 */

'use strict';



/**
 * @function Router
 * @description Отлавливает событие и отправляет данные в Controller
 **/
document.onclick = function () {
    var target = event.target,
        click = target;

    //Поиск элемента с data-атрибутами
    while (target !== event.currentTarget) {

        // Если target == null, значит был удалён элемент из DOM дерева.
        // Не знаю на сколько верное решение.
        if (target !== null) {
            if (target.hasAttribute('data-controller')) {

                var method = target.dataset.method;
                if (method === click.dataset.target) {
                    Controller(click, method, target);
                }

                break;
            }
            target = target.parentNode;
        }
        else {
            break;
        }
    }
};




/**
 * @function Controller
 * @description Отлавливает событие и отправляет данные в Controller
 * 
 * @param {object} click ...
 * @param {string} method ...
 * @param {object} target ...
 **/
function Controller(click, method, target) {
    if (method !== null) {
        window[method](click, target);
    }
}