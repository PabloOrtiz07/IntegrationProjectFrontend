var token = getCookie("Token");
let table = new DataTable('#services', {

    ajax: {
        url: `https://localhost:7056/api/Services?parameter=0&pageSize=10&pageToShow=1`, 
        dataSrc: "data.items",
        headers: { "Authorization": "Bearer " + token }
    },
    columns: [
        { data: 'id', title: 'Id' }, 
        { data: 'description', title: 'Description' }, 
        { data: 'isActive', title: 'Active' },
        { data: 'hourlyRate', title: 'Hourly Rate' },
        {
            data: function (data) {
                var buttons =
                    `<td><a href='javascript:EditService(${JSON.stringify(data)})'><i class="fa-solid fa-pen-to-square editService"></i></a></td>` +
                    `<td><a href='javascript:DeleteService(${JSON.stringify(data.id)})'><i class="fa-solid fa-trash deleteService"></i></a></td>`
                return buttons;
            }
        }

    ]
});

function AddService() {
    $.ajax({
        type: "POST",
        url: "/Services/ServicesAddPartial", 
        data: "",
        contentType: 'application/json',
        'dataType': "html",
        success: function (result) {
            $('#servicesAddPartial').html(result); 
            $('#serviceModal').modal('show'); 
        }

    });
}

function EditService(data) {
    var id = data.id;
    $.ajax({
        type: "POST",
        url: `/Services/ServicesAddPartial`,
        data: JSON.stringify({ id: id }),
        contentType: 'application/json',
        'dataType': "html",
        success: function (result) {
            $('#servicesAddPartial').html(result); 
            $('#serviceModal').modal('show'); 
        }
    });
}

function DeleteService(id) { 
    Swal.fire({
        title: "Are you sure you want to delete this service?", 
        text: "This service will be deleted from the database", 
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Yes",
        cancelButtonText: "No"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: `https://localhost:7056/api/Services/${id}?parameter=0`,
                dataType: "json",
                headers: { "Authorization": "Bearer " + token },
                success: function (result) {
                    if (result.status == 200) {
                        toastr.success("The service was deleted successfully"); 
                        toastr.error("An error occurred while deleting service"); 
                    }
                },
                error: function () {
                    toastr.error("Error occurred while deleting service"); 
                }
            });
        }
    });
}
