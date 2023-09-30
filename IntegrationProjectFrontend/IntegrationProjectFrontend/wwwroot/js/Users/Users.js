var token = getCookie("Token");
let table = new DataTable('#users', {

    ajax: {
        url: `https://localhost:7056/api/Users?parameter=0&pageSize=10&pageToShow=1`,
        dataSrc: "data.items",
        headers: { "Authorization": "Bearer " + token }
    },
    columns: [
        { data: 'firstName', title: 'Nombre' },
        { data: 'lastName', title: 'Apellido' },
        { data: 'dni', title: 'Dni' },
        { data: 'email', title: 'Mail' },
        { data: 'roleId', title: 'Role' },
        {
            data: function (data) {
                var buttons =
                    `<td><a href='javascript:EditUser(${JSON.stringify(data)})'><i class="fa-solid fa-pen-to-square editUser"></i></a></td>` +
                    `<td><a href='javascript:DeleteUser(${JSON.stringify(data)})'><i class="fa-solid fa-trash deleteUser"></i></a></td>`;
                return buttons;
            }
        }

    ]
});

function AddUser() {
    $.ajax({
        type: "GET",
        url: "/Users/UsersAddPartial",
        data: "",
        contentType: 'application/json',
        'dataType': "html",
        success: function (result) {
            $('#usersAddPartial').html(result);
            $('#userModal').modal('show');
        }

    });
}

function EditUser(data) {
    $.ajax({
        type: "PUT",
        url: "/Users/UsersAddPartial",
        data: JSON.stringify(data),
        contentType: 'application/json',
        'dataType': "html",
        success: function (result) {
            $('#usersAddPartial').html(result);
            $('#userModal').modal('show');
        }

    });
}
/*
function DeleteUser(data) {
    $.ajax({
        type: "GET",
        url: "/Users/DeleteUser",
        data: JSON.stringify(data),
        'dataType': "html",
        success: function (result) {

        }

    });
}*/