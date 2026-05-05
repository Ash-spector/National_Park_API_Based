var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/NationalPark/GetAll",
            "type": "GET",
            "dataType": "json"
        },
        "columns": [
            { "data": "name", "width": "40%" },
            { "data": "state", "width": "40%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                    <div class="text-center">
                        <a href="/NationalPark/Upsert/${data}" class="btn btn-success text-white" style="cursor:pointer; width:70px;">
                            <i class="fas fa-edit"></i>
                        </a>
                        <a class="btn btn-danger" onclick="Delete('/NationalPark/Delete/${data}')" style="cursor:pointer; width:70px;">
                            <i class="fas fa-trash-alt"></i>
                        </a>
                    </div>
                    `;
                }
            }
        ]
    });
}

function Delete(url) {
    swal({
        title: "Want to Delete data?",
        text: "Delete information !!!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {

        if (willDelete) {
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (data) {

                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}