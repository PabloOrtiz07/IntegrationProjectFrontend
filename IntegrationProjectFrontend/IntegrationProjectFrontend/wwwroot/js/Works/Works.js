var token = getCookie("Token");
let table = new DataTable('#works', { // Change 'users' to 'works'

    ajax: {
        url: `https://localhost:7056/api/Works?parameter=0&pageSize=10&pageToShow=1`, // Update the URL for works
        dataSrc: "data.items",
        headers: { "Authorization": "Bearer " + token }
    },
    columns: [
        { data: 'id', title: 'Id' }, // Update the field names to match WorkDTO
        { data: 'date', title: 'Date' }, // Update the field names to match WorkDTO
        { data: 'hoursQuantity', title: 'Hours Quantity' }, // Update the field names to match WorkDTO
        { data: 'hourlyRate', title: 'Hourly Rate' }, // Update the field names to match WorkDTO
        { data: 'cost', title: 'Cost' }, // Update the field names to match WorkDTO
        { data: 'projectId', title: 'Project Id' }, // Update the field names to match WorkDTO
        { data: 'serviceId', title: 'Service Id' }, // Update the field names to match WorkDTO
        {
            data: function (data) {
                var buttons =
                    `<td><a href='javascript:EditWork(${JSON.stringify(data)})'><i class="fa-solid fa-pen-to-square editWork"></i></a></td>` +
                    `<td><a href='javascript:DeleteWork(${JSON.stringify(data.Id)})'><i class="fa-solid fa-trash deleteWork"></i></a></td>`
                return buttons;
            }
        }

    ]
});

function AddWork() { // Change function name to AddWork
    $.ajax({
        type: "POST",
        url: "/Works/WorksAddPartial", // Update the URL for works
        data: "",
        contentType: 'application/json',
        'dataType': "html",
        success: function (result) {
            $('#worksAddPartial').html(result); // Update the ID to 'worksAddPartial'
            $('#workModal').modal('show'); // Update modal ID to 'workModal'
        }
    });
}

function DeleteWork(id) {
    Swal.fire({ // Use Swal.fire to create an instance
        title: "Are you sure you want to delete this work?",
        text: "This work will be deleted from the database",
        icon: "warning",
        showCancelButton: true, // Add showCancelButton to display the cancel button
        confirmButtonText: "Yes",
        cancelButtonText: "No"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                type: "DELETE",
                url: `https://localhost:7056/api/Works/${id}?parameter=0`,
                dataType: "json",
                headers: { "Authorization": "Bearer " + token },
                success: function (result) {
                    if (result.status == 200) {
                        toastr.success("The work was deleted successfully");
                    } else {
                        toastr.error("An error occurred while deleting work");
                    }
                },
                error: function () {
                    toastr.error("Error occurred while deleting work");
                }
            });
        }
    });
}
