var token = getCookie("Token");
let table = new DataTable('#users', {

    ajax: {
        url: `https://localhost:7056/api/Users?parameter=0&pageSize=10&pageToShow=1`,
        dataSrc: "data.items",
        headers: { "Authorization": "Bearer " + token }
    },
    columns: [
        { data: 'id', title: 'Id' },
        { data: 'firstName', title: 'Nombre' },
        { data: 'lastName', title: 'Apellido' },
        { data: 'dni', title: 'Dni' },
        { data: 'email', title: 'Mail' },
        { data: 'roleId', title: 'Role' },
        {
            data: function (data) {
                var buttons =
                    `<td><a href='javascript:EditUser(${JSON.stringify(data)})'><i class="fa-solid fa-pen-to-square editUser"></i></a></td>` +
                    `<td><a href='javascript:DeleteUser(${JSON.stringify(data.id)})'><i class="fa-solid fa-trash deleteUser"></i></a></td>`
                return buttons;
            }
        }

    ]
});

function AddUser() {
    $.ajax({
        type: "POST",
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
    var id = data.id;
    $.ajax({
        type: "POST",
        url: `/Users/UsersAddPartial`,
        data: JSON.stringify({ id: id }), 
        contentType: 'application/json',
        'dataType': "html",
        success: function (result) {
            $('#usersAddPartial').html(result);
            $('#userModal').modal('show');
        }
    });
}





function DeleteUser(id) {
    Swal.fire({ // Use Swal.fire to create an instance
        title: "Are you sure you want to delete this user?",
        text: "This user will be deleted from the database",
        icon: "warning",
        showCancelButton: true, // Add showCancelButton to display the cancel button
        confirmButtonText: "Yes",
        cancelButtonText: "No"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: `https://localhost:7056/api/Users/${id}?parameter=0`,
                dataType: "json",
                headers: { "Authorization": "Bearer " + token },
                success: function (result) {
                    if (result.status == 200) {
                        toastr.success("The user was deleted successfully");
                    } else {
                        toastr.error("An error occurred while deleting user");
                    }
                },
                error: function () {
                    toastr.error("Error occurred while deleting user");
                }
            });
        }
    });
}












