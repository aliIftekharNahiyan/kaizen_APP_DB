class Todo {

    constructor() {
        this.todoItems = [];
    }

    setup(defaultItems, parentElement) {
        let self = this;

        if (defaultItems) {
            for (var i = 0; i < defaultItems.length; i++) {
                addTodo(defaultItems[i].Name, defaultItems[i].Id);
            }
        }

        function renderTodo(todo) {
            const list = document.querySelector(`${parentElement} .js-todo-list`);
            const item = document.querySelector(`${parentElement} [data-key='${todo.id}']`);

            if (todo.deleted) {
                item.remove();
                return
            }

            const node = document.createElement("li");
            node.setAttribute('class', `list-group-item`);
            node.setAttribute('data-key', todo.id);
            node.innerHTML = `
                            <div class='row'>
                                <label class='col-md-10 col-form-label'>
                                    <input class="form-control editible-todo" type="text" value="${todo.text}">
                                </label>
                                <div class='col-md-2 float-end'>
                                    <a class="btn btn-danger delete-todo js-delete-todo float-end">Remove</a>
                                </div>
                                </button>
                            </div>
                          `;

            if (item) {
                list.replaceChild(node, item);
            } else {
                list.append(node);
            }
        }

        function addTodo(text, databaseId) {
            const todo = {
                text,
                checked: false,
                id: databaseId || Date.now(),
                deleted: false,
                databaseId
            };

            self.todoItems.push(todo);

            renderTodo(todo);
        }

        function deleteTodo(key) {
            const index = self.todoItems.findIndex(item => item.id === Number(key));
            const todo = {
                deleted: true,
                ...self.todoItems[index]
            };

            self.todoItems[index].deleted = true;
            /*self.todoItems = self.todoItems.filter(item => item.id !== Number(key));*/
            todo.deleted = true;

            renderTodo(todo);
        }

        const addButton = document.querySelector(`${parentElement} .add-item`);

        addButton.addEventListener('click', event => {
            event.preventDefault();

            const input = document.querySelector(`${parentElement} .js-todo-input`);

            const text = input.value.trim();
            if (text !== '') {
                addTodo(text);
                input.value = '';
                input.focus();
            }
        });

        const list = document.querySelector(`${parentElement} .js-todo-list`);
        list.addEventListener('click', event => {

            if (event.target.classList.contains('js-delete-todo')) {
                const itemKey = event.target.parentElement.parentElement.parentElement.dataset.key;
                deleteTodo(itemKey);
            }
        });

         
        const inputs = document.querySelector(`${parentElement} .editible-todo`);
        list.addEventListener('keyup', event => {
            const itemKey = event.target.parentElement.parentElement.parentElement.dataset.key;
            const index = self.todoItems.findIndex(item => item.id === Number(itemKey));

            self.todoItems[index].text = event.target.value.trim();
        });
    }

    getToDoItems() {
        return this.todoItems;
    }

    showErrors() {

    }
}