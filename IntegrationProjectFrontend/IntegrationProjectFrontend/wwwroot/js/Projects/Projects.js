var token = getCookie("Token");
let table = new DataTable('#projects', {

    ajax: {
        url: `https://localhost:7056/api/Projects?parameter=0&state=Pending&pageSize=10&pageToShow=1`, // Update the URL for projects
        dataSrc: "data.items",
        headers: { "Authorization": "Bearer " + token }
    },
    columns: [
        { data: 'id', title: 'Id' }, // Update the field names to match ProjectDTO
        { data: 'name', title: 'Name' }, // Update the field names to match ProjectDTO
        { data: 'address', title: 'Address' }, // Update the field names to match ProjectDTO
        { data: 'status', title: 'Status' }, // Update the field names to match ProjectDTO
        {
            data: function (data) {
                var buttons =
                    `<td><a href='javascript:EditProject(${JSON.stringify(data)})'><i class="fa-solid fa-pen-to-square editProject"></i></a></td>` +
                    `<td><a href='javascript:DeleteProject(${JSON.stringify(data.Id)})'><i class="fa-solid fa-trash deleteProject"></i></a></td>`
                return buttons;
            }
        }

    ]
});

function AddProject() {
    $.ajax({
        type: "POST",
        url: "/Projects/ProjectsAddPartial", // Update the URL for projects
        data: "",
        contentType: 'application/json',
        'dataType': "html",
        success: function (result) {
            $('#projectsAddPartial').html(result);
            $('#projectModal').modal('show'); // Update modal ID to 'projectModal'
        }
    });
}

function EditProject(data) {
    var id = data.Id;
    $.ajax({
        type: "POST",
        url: `/Projects/ProjectsAddPartial`, // Update the URL for projects
        data: JSON.stringify({ id: id }),
        contentType: 'application/json',
        'dataType': "html",
        success: function (result) {
            $('#projectsAddPartial').html(result);
            $('#projectModal').modal('show'); // Update modal ID to 'projectModal'
        }
    });
}

function DeleteProject(id) {
    Swal.fire({
        title: "Are you sure you want to delete this project?",
        text: "This project will be deleted from the database",
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes",
        cancelButtonText: "No"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: `https://localhost:7056/api/Projects/${id}?parameter=0`, // Update the URL for projects
                dataType: "json",
                headers: { "Authorization": "Bearer " + token },
                success: function (result) {
                    if (result.status == 200) {
                        toastr.success("The project was deleted successfully");
                    } else {
                        toastr.error("An error occurred while deleting project");
                    }
                },
                error: function () {
                    toastr.error("Error occurred while deleting project");
                }
            });
        }
    });
}
