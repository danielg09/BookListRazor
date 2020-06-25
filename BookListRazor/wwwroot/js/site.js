// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

var dataTable;

$(document).ready(function(){
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/book",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "author", "width": "20%" },
            { "data": "isbn", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<div class="text-center">
                                <a href="/BookList/Edit?id=${data}" class="btn btn-success text-white" style="cursor:pointer; width:70px;">
                                    Edit
                                </a>
                                &nbsp
                                <a href="/BookList/Upsert?id=${data}" class="btn btn-success text-white" style="cursor:pointer; width:70px;">
                                    Edit(Upsert)
                                </a>
                                &nbsp
                                <a onclick="Delete('/api/Book?id='+${data})" class="btn btn-danger text-white" style="cursor:pointer; width:70px;">
                                    Delete
                                </a>
                            </div>`;
                },"width": "40%"
            }
        ],
        "language": {
            "emptyTable": "No data found"
        },
        "width": "100%"
    });
}

function Delete(url) {
    
    Swal.fire({
        title: "Are you sure?",
        text: "Once deleted, you will not be able to recover",
        icon: "warning",
        showCancelButton: true
    }).then((willDelte) => {
        if (willDelte.value) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        dataTable.ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });

}